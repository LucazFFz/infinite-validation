using System.Text.RegularExpressions;
using InfiniteValidation.Extensions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class RegularExpressionSpecification<T> : Specification<T, string>
{ 
    private readonly Regex _regularExpression;

    public RegularExpressionSpecification(string regularExpression)
    {
        _regularExpression = new Regex(regularExpression);
    }

    public RegularExpressionSpecification(Regex regularExpression)
    {
        _regularExpression = regularExpression;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, string value)
    {
        value.Guard(GetSpecificationName());
        return _regularExpression.IsMatch(value);
    }

    public override string GetSpecificationName() => "RegularExpressionSpecification";
    
    public override string GetErrorMessage() => $"Value does not match {_regularExpression}";
}