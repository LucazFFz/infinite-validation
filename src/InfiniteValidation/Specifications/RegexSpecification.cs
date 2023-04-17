using System.Text.RegularExpressions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class RegexSpecification<T> : AbstractSpecification<T, string>
{ 
    private readonly Regex _regex;

    public RegexSpecification(string regex)
    {
        _regex = new Regex(regex);
    }

    public RegexSpecification(Regex regex)
    {
        _regex = regex;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, string value)
        => _regex.IsMatch(value);
    
    public override string GetSpecificationName() => "Match";
    
    public override string GetErrorMessage() => "this is a test message";
}