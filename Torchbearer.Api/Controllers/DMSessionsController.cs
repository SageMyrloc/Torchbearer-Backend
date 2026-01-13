using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Torchbearer.Application.Commands;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Queries;

namespace Torchbearer.Api.Controllers;

[ApiController]
[Route("api/dm/sessions")]
[Authorize(Roles = "DM,Admin")]
public class DMSessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DMSessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetPlayerId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpGet]
    public async Task<IActionResult> GetDMSessions()
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new GetDMSessionsQuery(playerId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new CreateSessionCommand(
                request.Title,
                request.Description,
                request.ScheduledAt,
                request.MaxPartySize,
                playerId));
            return CreatedAtAction(nameof(GetSessionById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(int id)
    {
        try
        {
            var result = await _mediator.Send(new GetSessionByIdQuery(id));
            if (result == null)
            {
                return NotFound(new { message = $"Session with id {id} not found" });
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSession(int id, [FromBody] UpdateSessionRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new UpdateSessionCommand(
                id,
                request.Title,
                request.Description,
                request.ScheduledAt,
                request.MaxPartySize,
                playerId));
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/start")]
    public async Task<IActionResult> StartSession(int id)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new StartSessionCommand(id, playerId));
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteSession(int id, [FromBody] CompleteSessionRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new CompleteSessionCommand(
                id,
                request.GoldReward,
                request.ExperienceReward,
                playerId));
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelSession(int id)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new CancelSessionCommand(id, playerId));
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{sessionId}/attendees/{characterId}")]
    public async Task<IActionResult> RemoveAttendee(int sessionId, int characterId)
    {
        try
        {
            var playerId = GetPlayerId();
            await _mediator.Send(new RemoveAttendeeCommand(sessionId, characterId, playerId));
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
