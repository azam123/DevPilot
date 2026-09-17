using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/json/format", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var input = await reader.ReadToEndAsync();
    try
    {
        using var doc = JsonDocument.Parse(input);
        return Results.Ok(new { output = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }) });
    }
    catch (JsonException ex) { return Results.BadRequest(new { error = ex.Message }); }
});

app.MapPost("/api/base64", async (Base64Request body) =>
{
    try
    {
        if (body.Decode) return Results.Ok(new { output = Encoding.UTF8.GetString(Convert.FromBase64String(body.Value)) });
        return Results.Ok(new { output = Convert.ToBase64String(Encoding.UTF8.GetBytes(body.Value)) });
    }
    catch (Exception ex) { return Results.BadRequest(new { error = ex.Message }); }
});

app.MapGet("/api/guid", () => Results.Ok(new { output = Guid.NewGuid().ToString() }));
app.MapGet("/api/timestamp", () => Results.Ok(new { unix = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), utc = DateTimeOffset.UtcNow }));

app.MapPost("/api/hash", async (HashRequest body) =>
{
    using HashAlgorithm algorithm = body.Algorithm?.ToUpperInvariant() switch
    {
        "SHA512" => SHA512.Create(),
        _ => SHA256.Create()
    };
    var hash = Convert.ToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(body.Value)));
    return Results.Ok(new { output = hash });
});

app.Run();
record Base64Request(string Value, bool Decode);
record HashRequest(string Value, string? Algorithm);
