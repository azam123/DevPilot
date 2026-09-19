using Microsoft.AspNetCore.Mvc;

namespace DevPilot.Api;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        status = "healthy",
        service = "DevPilot",
        utc = DateTimeOffset.UtcNow
    });
}
