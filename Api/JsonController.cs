using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevPilot.Api;

[ApiController]
[Route("api/json")]
public sealed class JsonController : ControllerBase
{
    [HttpPost("format")]
    [SwaggerOperation(Summary = "Format JSON", Description = "Validates JSON and returns indented JSON.")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Format([FromBody] JsonElement input)
    {
        return Ok(new { output = JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true }) });
    }
}
