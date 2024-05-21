namespace Proposal.Api.Events;
public static class HandlerRegistration
{
    public static void RegisterHandlers(this IServiceCollection services)
    {
        services.AddSingleton<BasicEventHandler>();
        services.AddSingleton<StringEventHandler>();
        services.AddSingleton<IntEventHandler>();
    }
}