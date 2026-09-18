namespace DevPilot.Core;

/// <summary>Small, dependency-free utilities suitable for reuse in .NET applications.</summary>
public static class DeveloperUtilities
{
    public static string ToBase64(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
    }

    public static string FromBase64(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value));
    }

    public static string Sha256(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var bytes = System.Security.Cryptography.SHA256.HashData(
            System.Text.Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }

    public static string Sha512(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var bytes = System.Security.Cryptography.SHA512.HashData(
            System.Text.Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}
