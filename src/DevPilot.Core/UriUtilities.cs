namespace DevPilot.Core;

/// <summary>
/// Dependency-free helpers for validating and inspecting HTTP/HTTPS URLs.
/// </summary>
public static class UriUtilities
{
    /// <summary>
    /// Tries to parse an absolute HTTP or HTTPS URL.
    ///
    /// Relative URLs and other URI schemes are rejected so callers can safely
    /// use this helper when an external web address is required.
    /// </summary>
    public static bool TryParseHttpUrl(string value, out Uri? uri)
    {
        uri = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!Uri.TryCreate(value.Trim(), UriKind.Absolute, out var candidate))
            return false;

        if (!string.Equals(candidate.Scheme, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(candidate.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
            return false;

        if (string.IsNullOrWhiteSpace(candidate.Host))
            return false;

        uri = candidate;
        return true;
    }

    /// <summary>Returns the normalized absolute HTTP/HTTPS URL or null when invalid.</summary>
    public static string? NormalizeHttpUrl(string value)
    {
        return TryParseHttpUrl(value, out var uri) ? uri!.AbsoluteUri : null;
    }
}
