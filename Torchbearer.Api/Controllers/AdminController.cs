using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Torchbearer.Application.Commands;
using Torchbearer.Application.Queries;

namespace Torchbearer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("players")]
    public async Task<IActionResult> GetAllPlayers()
    {
        try
        {
            var result = await _mediator.Send(new GetAllPlayersQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        try
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("players/{playerId}/roles/{roleId}")]
    public async Task<IActionResult> AssignRole(int playerId, int roleId)
    {
        try
        {
            await _mediator.Send(new AssignRoleCommand(playerId, roleId));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("players/{playerId}/roles/{roleId}")]
    public async Task<IActionResult> RemoveRole(int playerId, int roleId)
    {
        try
        {
            await _mediator.Send(new RemoveRoleCommand(playerId, roleId));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("players/{id}/slots")]
    public async Task<IActionResult> UpdatePlayerSlots(int id, [FromBody] UpdatePlayerSlotsRequest request)
    {
        try
        {
            var result = await _mediator.Send(new UpdatePlayerSlotsCommand(id, request.MaxSlots));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("characters/{id}/gold")]
    public async Task<IActionResult> UpdateCharacterGold(int id, [FromBody] UpdateCharacterGoldRequest request)
    {
        try
        {
            var result = await _mediator.Send(new UpdateCharacterGoldCommand(id, request.Gold));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("characters/{id}/experience")]
    public async Task<IActionResult> UpdateCharacterExperience(int id, [FromBody] UpdateCharacterExperienceRequest request)
    {
        try
        {
            var result = await _mediator.Send(new UpdateCharacterExperienceCommand(id, request.ExperiencePoints));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("characters/{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteCharacterCommand(id));
            return Ok(new { success = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public record UpdatePlayerSlotsRequest(int MaxSlots);
public record UpdateCharacterGoldRequest(decimal Gold);
public record UpdateCharacterExperienceRequest(int ExperiencePoints);
