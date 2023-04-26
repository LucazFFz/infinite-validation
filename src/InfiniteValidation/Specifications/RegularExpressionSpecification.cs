using System.Text.RegularExpressions;
using InfiniteValidation.Internal;

namespace InfiniteValidation.Specifications;

public class RegularExpressionSpecification<T> : Specification<T, string>
{
    private readonly Regex _regex;

    public RegularExpressionSpecification(Regex regex)
    {
        regex.Guard(nameof(regex));
        _regex = regex;
    }
    
    public RegularExpressionSpecification(string regex)
    {
        regex.Guard(nameof(regex));
        _regex = new Regex(regex);
    }
    
    public RegularExpressionSpecification(string regex, RegexOptions options)
    {
        regex.Guard(nameof(regex));
        _regex = new Regex(regex, options);
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, string value)
    {
        value.Guard(nameof(value));
        return _regex.IsMatch(value);
    }

    public override string GetSpecificationName() => "RegularExpressionSpecification";
    
    public override string GetErrorMessage() => $"Value does not match {_regex}";
}