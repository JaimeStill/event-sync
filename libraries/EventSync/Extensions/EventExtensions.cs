using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace EventSync.Extensions;
public static class EventExtensions
{
    public static HubConnectionBuilder ConfigureJsonFormat(this HubConnectionBuilder builder) =>
        builder
            .AddJsonProtocol(SignalRJsonOptions);   

    public static ISignalRServerBuilder ConfigureSignalRServices(this WebApplicationBuilder builder) =>
        builder
            .Services
            .AddSignalR()
            .AddJsonProtocol(SignalRJsonOptions);

    static JsonSerializerOptions ConfigureJsonOptions(JsonSerializerOptions options)
    {
        options.Converters.Add(new JsonStringEnumConverter());
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        return options;
    }

    static Action<JsonHubProtocolOptions> SignalRJsonOptions =>
        (JsonHubProtocolOptions options) =>
            ConfigureJsonOptions(options.PayloadSerializerOptions);
}