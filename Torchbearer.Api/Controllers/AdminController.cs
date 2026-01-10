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
}
