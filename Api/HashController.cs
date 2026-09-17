using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevPilot.Api;

public sealed record HashRequest(string Value, string Algorithm = "SHA256");

[ApiController]
[Route("api/hash")]
public sealed class HashController : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Generate a cryptographic hash", Description = "Generates an uppercase hexadecimal SHA256 or SHA512 hash from UTF-8 text.")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public IActionResult Generate([FromBody] HashRequest? request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Value))
            return BadRequest(new { error = "Value is required." });

        using HashAlgorithm algorithm = request.Algorithm.Trim().ToUpperInvariant() switch
        {
            "SHA512" => SHA512.Create(),
            "SHA256" => SHA256.Create(),
            _ => null!
        };

        if (algorithm is null)
            return BadRequest(new { error = "Supported algorithms are SHA256 and SHA512." });

        var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(request.Value));
        return Ok(new { algorithm = request.Algorithm.ToUpperInvariant(), output = Convert.ToHexString(hash) });
    }
}
