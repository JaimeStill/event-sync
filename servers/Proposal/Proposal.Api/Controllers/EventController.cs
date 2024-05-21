using EventSync;
using Microsoft.AspNetCore.Mvc;
using Proposal.Api.Events;

namespace Proposal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController(WorkflowEventListener listener) : ControllerBase
{
    readonly WorkflowEventListener listener = listener;

    [HttpGet("[action]")]
    public async Task<IActionResult> Ping()
    {
        await listener.Ping();
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncString()
    {
        await listener.Sync(
            new EventMessage
            {
                Id = 1,
                Type = typeof(string).FullName ?? "System.String",
                Message = "Syncing string from Proposal service"
            }
        );

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncInt()
    {
        await listener.Sync(
            new EventMessage
            {
                Id = 2,
                Type = typeof(int).FullName ?? "System.Int32",
                Message = "Syncing integer from Proposal service"
            }
        );

        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> SyncDynamic()
    {
        await listener.Sync(
            new EventMessage
            {
                Id = 3,
                Type = "dynamic",
                Message = "Syncing dynamic from Proposal service"
            }
        );

        return Ok();
    }
}