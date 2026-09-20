namespace DevPilot.Core;

/// <summary>
/// URL encoding helpers for safely moving text through query strings and URLs.
/// </summary>
public static class EncodingUtilities
{
    /// <summary>Encodes a value using RFC 3986-style percent encoding.</summary>
    public static string UrlEncode(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return Uri.EscapeDataString(value);
    }

    /// <summary>Decodes a percent-encoded URL component.</summary>
    public static string UrlDecode(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return Uri.UnescapeDataString(value);
    }
}
