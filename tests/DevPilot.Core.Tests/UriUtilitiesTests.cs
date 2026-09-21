using DevPilot.Core;

namespace DevPilot.Core.Tests;

public sealed class UriUtilitiesTests
{
    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://localhost:5000/api/health?ready=true")]
    public void TryParseHttpUrl_AcceptsAbsoluteHttpUrls(string value)
    {
        var result = UriUtilities.TryParseHttpUrl(value, out var uri);

        Assert.True(result);
        Assert.NotNull(uri);
    }

    [Theory]
    [InlineData("/relative/path")]
    [InlineData("ftp://example.com/file.txt")]
    [InlineData("not a url")]
    [InlineData("")]
    public void TryParseHttpUrl_RejectsUnsupportedOrInvalidValues(string value)
    {
        var result = UriUtilities.TryParseHttpUrl(value, out var uri);

        Assert.False(result);
        Assert.Null(uri);
    }

    [Fact]
    public void NormalizeHttpUrl_TrimsAndReturnsAbsoluteUri()
    {
        var result = UriUtilities.NormalizeHttpUrl("  https://example.com/path  ");

        Assert.Equal("https://example.com/path", result);
    }

    [Fact]
    public void NormalizeHttpUrl_ReturnsNullForInvalidValue()
    {
        Assert.Null(UriUtilities.NormalizeHttpUrl("javascript:alert(1)"));
    }
}
