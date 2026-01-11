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
public class CharactersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;

    public CharactersController(IMediator mediator, IWebHostEnvironment environment, IConfiguration configuration)
    {
        _mediator = mediator;
        _environment = environment;
        _configuration = configuration;
    }

    private int GetPlayerId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    [HttpGet("my")]
    public async Task<IActionResult> GetMyCharacters()
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new GetMyCharactersQuery(playerId));
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacterById(int id)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new GetCharacterByIdQuery(id, playerId));
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

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new CreateCharacterCommand(playerId, request.Name));
            return CreatedAtAction(nameof(GetCharacterById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(int id, [FromBody] UpdateCharacterRequest request)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new UpdateCharacterCommand(id, playerId, request.Name));
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

    [HttpPost("{id}/retire")]
    public async Task<IActionResult> RetireCharacter(int id)
    {
        try
        {
            var playerId = GetPlayerId();
            var result = await _mediator.Send(new RetireCharacterCommand(id, playerId));
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

    [HttpPost("{id}/image")]
    public async Task<IActionResult> UploadCharacterImage(int id, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            var maxSizeBytes = _configuration.GetValue<long>("FileStorage:MaxImageSizeBytes", 2097152);
            if (file.Length > maxSizeBytes)
            {
                return BadRequest(new { message = $"File size exceeds maximum allowed size of {maxSizeBytes / 1024 / 1024}MB" });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new { message = "Invalid file type. Allowed types: jpg, jpeg, png" });
            }

            var playerId = GetPlayerId();

            var existingCharacter = await _mediator.Send(new GetCharacterByIdQuery(id, playerId));

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "assets", "characters");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (!string.IsNullOrEmpty(existingCharacter.ImageFileName))
            {
                var oldFilePath = Path.Combine(uploadsFolder, existingCharacter.ImageFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            var fileName = $"{id}_{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var result = await _mediator.Send(new UploadCharacterImageCommand(id, playerId, fileName));
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
}
