using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevPilot.Api;

[ApiController]
[Route("api/utility")]
public sealed class UtilityController : ControllerBase
{
    [HttpGet("guid")]
    [SwaggerOperation(Summary = "Generate a GUID")]
    public IActionResult Guid() => Ok(new { output = System.Guid.NewGuid() });

    [HttpGet("timestamp")]
    [SwaggerOperation(Summary = "Get current UTC timestamp", Description = "Returns Unix seconds and an ISO-8601 UTC timestamp.")]
    public IActionResult Timestamp()
    {
        var now = DateTimeOffset.UtcNow;
        return Ok(new { unix = now.ToUnixTimeSeconds(), utc = now });
    }
}
