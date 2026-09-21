using DevPilot.Core;
using Microsoft.AspNetCore.Mvc;

namespace DevPilot.Api;

/// <summary>Validates and inspects absolute HTTP/HTTPS URLs without making network calls.</summary>
[ApiController]
[Route("api/validation")]
public sealed class UrlValidationController : ControllerBase
{
    /// <summary>Validates a URL and returns useful parsed components.</summary>
    [HttpPost("url")]
    public ActionResult<UrlValidationResponse> Validate([FromBody] UrlValidationRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Value))
            return BadRequest(new { error = "value is required" });

        if (request.Value.Length > 2048)
            return BadRequest(new { error = "value must be 2048 characters or fewer" });

        if (!UriUtilities.TryParseHttpUrl(request.Value, out var uri))
        {
            return BadRequest(new
            {
                error = "value must be an absolute HTTP or HTTPS URL"
            });
        }

        return Ok(new UrlValidationResponse(
            IsValid: true,
            Url: uri!.AbsoluteUri,
            Scheme: uri.Scheme,
            Host: uri.Host,
            Port: uri.Port,
            Path: uri.AbsolutePath,
            HasQuery: !string.IsNullOrEmpty(uri.Query),
            HasFragment: !string.IsNullOrEmpty(uri.Fragment)));
    }
}

public sealed record UrlValidationRequest(string? Value);

public sealed record UrlValidationResponse(
    bool IsValid,
    string Url,
    string Scheme,
    string Host,
    int Port,
    string Path,
    bool HasQuery,
    bool HasFragment);
