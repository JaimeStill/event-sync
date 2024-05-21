# Event Sync

> Currently in development

In previous projects where I've implemented SignalR-based event synchronization, the implementations ended up feeling cumbersome or were not the most performant. Causes for this were:

* Targeting an event hub for every entity type in an API service, which lead to entirely too many open channels in any given context.

* Due to the event hubs being tied to entities, the level of generic constraints on both the sync infrastructure and the API services themselves made it very cumbersome.

* Event messages caused heavy traffic loads because each message contained the full object that was affected.

* Because of the amount of event hubs being defined, an equivalent number of event listener clients needed to be initialized and managed in applications and cross-service integrations.

* Each service had to define a significant amount of public API infrastructure to facilitate cross-service event sync.

This project seeks to optimize and simplify distributed event syncing with SignalR. It will do so by implementing the following adjustments:

* Each API service will have only one [`EventHub`](./libraries/EventSync/Server/EventHub.cs) endpoint that handles all events for the service.

* [`EventMessage`](./libraries/EventSync/EventMessage.cs) records will only capture the ID of the affected object, it's type, and an optional message communicating how the object was affected.

* An [`IEventHandler`](./libraries/EventSync/Client/IEventHandler.cs) determines how to handle each event given type. A concrete instance will be defined for each concrete `EventMessage` type emitted by an `EventHub` and registered as a singleton service.

* An [`IEventResolver`](./libraries/EventSync/Client/IEventResolver.cs) resolves the `IEventHandler` to use based on the received `EventMessage.Type`. A concrete instance of `IEventResolver` will be defined for each `EventHub` and registered as a singleton service.

    ```cs
    public IEventHandler Resolve(EventMessage message, IServiceProvider provider) => message.Type switch
    {
        "System.String" => provider.GetRequiredService<StringEventHandler>(),
        "System.Int32" => provider.GetRequiredService<IntEventHandler>(),
        _ => provider.GetRequiredService<BasicEventHandler>()
    }
    ```

* All incoming events for an `EventHub` will be handled by a single [`EventListener`](./libraries/EventSync/Client/EventListener.cs). The `EventListener` must be registered with `builder.Services.AddSingleton<EventListener<ER>>` where `ER` is the `IEventResolver` implementation for the `EventHub`.

* The connection to the remote `EventHub` will be managed through an [`EventListenerConnector`](./libraries/EventSync/Client/EventListenerConnector.cs). All `IEvent*` services for an `EventHub` must be registered before the `EventListenerConnector`.

* Because `EventListener` has been simplified and standardized at the library level, the only event infrastructure you need to export are any custom `EventMessage` implementations used by the `EventHub`. You just need to ensure the integrating service has CORS access to the target service, knows the `EventHub` endpoint, and has properly configured `IEventResolver` and `IEventHandler` instances to handle the events.

* For web applications, only a single listener for each `EventHub` will be registered at the root of the application. The incoming events and messages will be distributed throughout the application using an RxJS observable. This observable can be filtered within any of its various subscription contexts so that the app optimizes how it handles event message flow.