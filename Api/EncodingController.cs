using DevPilot.Core;
using Microsoft.AspNetCore.Mvc;

namespace DevPilot.Api;

/// <summary>HTTP endpoints for URL component encoding and decoding.</summary>
[ApiController]
[Route("api/encoding")]
public sealed class EncodingController : ControllerBase
{
    /// <summary>Encodes text so it can safely be used as a URL component.</summary>
    [HttpPost("url/encode")]
    public ActionResult<EncodingResponse> Encode([FromBody] EncodingRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Value))
            return BadRequest(new { error = "value is required" });

        return Ok(new EncodingResponse(EncodingUtilities.UrlEncode(request.Value)));
    }

    /// <summary>Decodes a percent-encoded URL component.</summary>
    [HttpPost("url/decode")]
    public ActionResult<EncodingResponse> Decode([FromBody] EncodingRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Value))
            return BadRequest(new { error = "value is required" });

        try
        {
            return Ok(new EncodingResponse(EncodingUtilities.UrlDecode(request.Value)));
        }
        catch (UriFormatException)
        {
            return BadRequest(new { error = "value is not a valid encoded URL component" });
        }
    }
}

public sealed record EncodingRequest(string? Value);
public sealed record EncodingResponse(string Value);
