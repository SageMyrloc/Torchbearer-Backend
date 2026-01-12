using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Torchbearer.Application.Commands;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Queries;

namespace Torchbearer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetPlayerId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpGet]
    public async Task<IActionResult> GetUpcomingSessions()
    {
        try
        {
            var result = await _mediator.Send(new GetUpcomingSessionsQuery());
            return Ok(result);
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

    [HttpGet("my")]
    public async Task<IActionResult> GetMySessions()
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new GetMySessionsQuery(playerId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/signup")]
    public async Task<IActionResult> SignUpForSession(int id, [FromBody] SignUpRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new SignUpForSessionCommand(id, request.CharacterId, playerId));
            return Ok(new { success = result });
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

    [HttpDelete("{id}/signup/{characterId}")]
    public async Task<IActionResult> WithdrawFromSession(int id, int characterId)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new WithdrawFromSessionCommand(id, characterId, playerId));
            return Ok(new { success = result });
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
