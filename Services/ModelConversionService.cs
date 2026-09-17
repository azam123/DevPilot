using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace DevPilot.Services;

public interface IModelConversionService
{
    string JsonToCSharp(string input, string root);
    string XmlToCSharp(string input, string root);
    string CSharpToJson(string input);
    string CSharpToXml(string input, string root);
}

public sealed class ModelConversionService : IModelConversionService
{
    public string JsonToCSharp(string input, string root)
    {
        using var document = JsonDocument.Parse(input);
        var builder = new StringBuilder();

        BuildJson(document.RootElement, root, builder, new HashSet<string>());

        return builder.ToString();
    }

    private void BuildJson(
        JsonElement element,
        string name,
        StringBuilder builder,
        HashSet<string> generatedTypes)
    {
        if (element.ValueKind != JsonValueKind.Object || !generatedTypes.Add(name))
        {
            return;
        }

        var properties = new List<(string Name, string Type)>();

        foreach (var property in element.EnumerateObject())
        {
            var propertyName = Safe(property.Name);
            var propertyType = GetJsonType(
                property.Value,
                propertyName,
                builder,
                generatedTypes);

            properties.Add((propertyName, propertyType));
        }

        builder.AppendLine($"public sealed class {Safe(name)}");
        builder.AppendLine("{");

        foreach (var property in properties)
        {
            builder.AppendLine(
                $"    public {property.Type} {property.Name} {{ get; set; }}");
        }

        builder.AppendLine("}");
        builder.AppendLine();
    }

    private string GetJsonType(
        JsonElement element,
        string name,
        StringBuilder builder,
        HashSet<string> generatedTypes)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => "string?",
            JsonValueKind.Number => element.TryGetInt64(out _)
                ? "long"
                : "double",
            JsonValueKind.True or JsonValueKind.False => "bool",
            JsonValueKind.Array => element.GetArrayLength() == 0
                ? "List<object>"
                : $"List<{GetJsonType(element[0], $"{name}Item", builder, generatedTypes)}>",
            JsonValueKind.Object => BuildObjectType(
                element,
                name,
                builder,
                generatedTypes),
            _ => "object?"
        };
    }

    private string BuildObjectType(
        JsonElement element,
        string name,
        StringBuilder builder,
        HashSet<string> generatedTypes)
    {
        var typeName = Safe(name);
        BuildJson(element, typeName, builder, generatedTypes);

        return typeName;
    }

    public string XmlToCSharp(string input, string root)
    {
        var builder = new StringBuilder();
        var document = XElement.Parse(input);

        BuildXml(document, root, builder, new HashSet<string>());

        return builder.ToString();
    }

    private void BuildXml(
        XElement element,
        string name,
        StringBuilder builder,
        HashSet<string> generatedTypes)
    {
        if (!generatedTypes.Add(name))
        {
            return;
        }

        var childGroups = element
            .Elements()
            .GroupBy(child => child.Name.LocalName)
            .ToList();

        builder.AppendLine($"public sealed class {Safe(name)}");
        builder.AppendLine("{");

        foreach (var group in childGroups)
        {
            var child = group.First();
            var propertyType = child.HasElements
                ? Safe(child.Name.LocalName)
                : "string?";

            if (group.Count() > 1)
            {
                propertyType = $"List<{propertyType}>";
            }

            if (child.HasElements)
            {
                BuildXml(
                    child,
                    child.Name.LocalName,
                    builder,
                    generatedTypes);
            }

            builder.AppendLine(
                $"    public {propertyType} {Safe(group.Key)} {{ get; set; }}");
        }

        builder.AppendLine("}");
        builder.AppendLine();
    }

    public string CSharpToJson(string input)
    {
        var result = new Dictionary<string, object?>();
        var pattern =
            @"(?:public|private|internal)\s+([\w<>?,\[\]]+)\s+(\w+)\s*\{\s*get;";

        foreach (Match match in Regex.Matches(
            input,
            pattern,
            RegexOptions.IgnoreCase))
        {
            result[match.Groups[2].Value] =
                GetDefaultValue(match.Groups[1].Value);
        }

        return JsonSerializer.Serialize(
            result,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
    }

    public string CSharpToXml(string input, string root)
    {
        using var document = JsonDocument.Parse(CSharpToJson(input));
        var rootElement = new XElement(root);

        foreach (var property in document.RootElement.EnumerateObject())
        {
            rootElement.Add(
                new XElement(property.Name, property.Value.ToString()));
        }

        return rootElement.ToString();
    }

    private static object? GetDefaultValue(string type)
    {
        if (type.Contains("int", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        if (type.Contains("bool", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (type.StartsWith("List", StringComparison.OrdinalIgnoreCase))
        {
            return Array.Empty<object>();
        }

        return string.Empty;
    }

    private static string Safe(string name)
    {
        var sanitizedName = Regex.Replace(name, "[^A-Za-z0-9_]", string.Empty);

        if (string.IsNullOrWhiteSpace(sanitizedName))
        {
            sanitizedName = "Model";
        }

        return char.ToUpperInvariant(sanitizedName[0]) + sanitizedName[1..];
    }
}
