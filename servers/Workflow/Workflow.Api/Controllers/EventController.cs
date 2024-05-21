using EventSync;
using EventSync.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Workflow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(IHubContext<EventHub, IEventHub> events) : ControllerBase
{
    readonly IHubContext<EventHub, IEventHub> events = events;

    [HttpGet("[action]")]
    public async Task<IActionResult> Ping()
    {
        await events.Clients.All.Ping();

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Sync()
    {
        await events
            .Clients
            .All
            .Sync(new EventMessage
            {
                Id = 0,
                Type = "System.String",
                Message = "Testing EventSync library"
            });

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncString()
    {
        await events
            .Clients
            .All
            .Sync(new EventMessage
            {
                Id = 0,
                Type = typeof(string).FullName ?? "System.String",
                Message = "Trigger string sync from Workflow service"
            });

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncInt()
    {
        await events
            .Clients
            .All
            .Sync(new EventMessage
            {
                Id = 0,
                Type = typeof(int).FullName ?? "System.Int32",
                Message = "Trigger integer sync from Workflow service"
            });

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncDynamic()
    {
        await events
            .Clients
            .All
            .Sync(new EventMessage
            {
                Id = 0,
                Type = "dynamic",
                Message = "Trigger dynamic sync from Workflow service"
            });

        return Ok();
    }
}