namespace DevPilot.Services;

public static class ConversionInputValidator
{
    public static void Validate(string? input, string? root, string operation)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input is required.", nameof(input));

        if (input.Length > 1_000_000)
            throw new ArgumentException("Input must be 1 MB or smaller.", nameof(input));

        if (string.IsNullOrWhiteSpace(root))
            throw new ArgumentException("Root is required.", nameof(root));

        if (root.Length > 100)
            throw new ArgumentException("Root must be 100 characters or fewer.", nameof(root));

        if (!char.IsLetter(root[0]) && root[0] != '_')
            throw new ArgumentException("Root must start with a letter or underscore.", nameof(root));

        if (root.Any(c => !char.IsLetterOrDigit(c) && c != '_'))
            throw new ArgumentException("Root may contain only letters, digits, and underscores.", nameof(root));
    }
}
