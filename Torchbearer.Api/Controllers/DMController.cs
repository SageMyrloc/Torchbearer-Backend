using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Torchbearer.Application.Commands;
using Torchbearer.Application.Queries;

namespace Torchbearer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "DM,Admin")]
public class DMController : ControllerBase
{
    private readonly IMediator _mediator;

    public DMController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("characters/pending")]
    public async Task<IActionResult> GetPendingCharacters()
    {
        try
        {
            var result = await _mediator.Send(new GetPendingCharactersQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("characters")]
    public async Task<IActionResult> GetAllCharacters()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCharactersQuery());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("characters/{id}/approve")]
    public async Task<IActionResult> ApproveCharacter(int id)
    {
        try
        {
            var result = await _mediator.Send(new ApproveCharacterCommand(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("characters/{id}/kill")]
    public async Task<IActionResult> KillCharacter(int id)
    {
        try
        {
            var result = await _mediator.Send(new KillCharacterCommand(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("characters/{id}/activate")]
    public async Task<IActionResult> ActivateCharacter(int id)
    {
        try
        {
            var result = await _mediator.Send(new ActivateCharacterCommand(id));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
