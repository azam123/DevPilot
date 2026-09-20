using DevPilot.Core;

namespace DevPilot.Core.Tests;

public sealed class DeveloperUtilitiesTests
{
    [Fact]
    public void Base64_RoundTripsUtf8Text()
    {
        const string value = "Hello, DevPilot 👋";

        var encoded = DeveloperUtilities.ToBase64(value);
        var decoded = DeveloperUtilities.FromBase64(encoded);

        Assert.Equal(value, decoded);
    }

    [Fact]
    public void Hashing_IsDeterministic()
    {
        Assert.Equal(
            "2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824",
            DeveloperUtilities.Sha256("hello"));
    }

    [Fact]
    public void UrlEncoding_RoundTripsReservedCharacters()
    {
        const string value = "name=Azam & role=Principal Engineer";

        var encoded = EncodingUtilities.UrlEncode(value);
        var decoded = EncodingUtilities.UrlDecode(encoded);

        Assert.Equal(value, decoded);
        Assert.Contains("%", encoded);
    }

    [Fact]
    public void NullInput_IsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => DeveloperUtilities.ToBase64(null!));
        Assert.Throws<ArgumentNullException>(() => EncodingUtilities.UrlEncode(null!));
    }
}
