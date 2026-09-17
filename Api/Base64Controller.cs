using System.Text;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevPilot.Api;

public sealed record Base64Request(string Value, bool Decode = false);

[ApiController]
[Route("api/base64")]
public sealed class Base64Controller : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Encode or decode Base64", Description = "Set Decode to true to decode; otherwise encodes UTF-8 text.")]
    public IActionResult Convert([FromBody] Base64Request request)
    {
        try
        {
            var output = request.Decode
                ? Encoding.UTF8.GetString(System.Convert.FromBase64String(request.Value))
                : System.Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Value));
            return Ok(new { output });
        }
        catch (FormatException ex) { return BadRequest(new { error = ex.Message }); }
    }
}
