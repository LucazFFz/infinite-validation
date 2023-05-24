using System.Text.RegularExpressions;
using InfiniteValidation.Internal;

namespace InfiniteValidation;

public partial class MessageBuilder
{
    public const string PropertyName = "PropertyName";

    public const string PropertyValue = "PropertyValue";
    
    private readonly Dictionary<string, object?> _placeholderValues = new(2);
    
    public MessageBuilder Append(string name, object? value)
    {
        _placeholderValues[name] = value;
        return this;
    }

    public MessageBuilder AppendPropertyName(string propertyName)
    {
        Append(PropertyName, propertyName);
        return this;
    }
    
    public MessageBuilder AppendAttemptedValue(object? value)
    {
        Append(PropertyValue, value);
        return this;
    }

    public virtual string Build(string template)
    {
        template.Guard(nameof(template));
        return KeyRegex().Replace(template, MatchEvaluator!);
    }

    private string? MatchEvaluator(Match match)
    {
        var key = match.Groups[1].Value;
        var success = _placeholderValues.TryGetValue(key, out var value);
        
        if (!success) return match.Value;
        
        var format = match.Groups[2].Success ? $"{{0:{match.Groups[2].Value}}}" : null;
        
        return format is not null ? string.Format(format, value) : value?.ToString();
    }

    [GeneratedRegex("{([^{}:]+)(?::([^{}]+))?}", RegexOptions.Compiled)]
    private static partial Regex KeyRegex();
}