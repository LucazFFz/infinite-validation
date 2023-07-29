using System.Text.RegularExpressions;
using InfiniteValidation.Internal;

namespace InfiniteValidation.Specifications;

public sealed class RegexSpecification<T> : Specification<T, string>
{
    private readonly Regex _regex;

    public RegexSpecification(Regex regex)
    {
        regex.Guard(nameof(regex));
        _regex = regex;
        MessageBuilder.Append("Regex", regex);
    }
    
    public RegexSpecification(string regex)
    {
        regex.Guard(nameof(regex));
        _regex = new Regex(regex);
        MessageBuilder.Append("Regex", regex);
    }
    
    public RegexSpecification(string regex, RegexOptions options)
    {
        regex.Guard(nameof(regex));
        _regex = new Regex(regex, options);
        MessageBuilder.Append("Regex", regex);
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, string value)
    {
        value.Guard(nameof(value));
        return _regex.IsMatch(value);
    }

    public override string GetName() => "RegexSpecification";
    
    public override string GetMessageFormat() => "'{PropertyName}' must match regex: '{Regex}'.";
}