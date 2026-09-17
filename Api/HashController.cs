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
    [SwaggerOperation(Summary = "Generate a cryptographic hash", Description = "Supported algorithms: SHA256 and SHA512.")]
    public IActionResult Generate([FromBody] HashRequest request)
    {
        using HashAlgorithm algorithm = request.Algorithm.ToUpperInvariant() switch
        {
            "SHA512" => SHA512.Create(),
            "SHA256" => SHA256.Create(),
            _ => throw new ArgumentException("Supported algorithms are SHA256 and SHA512.")
        };
        return Ok(new { output = Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(request.Value))) });
    }
}
