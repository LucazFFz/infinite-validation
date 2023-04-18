using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class EqualSpecification<T, TProperty> : Specification<T, TProperty>
{ 
    private readonly TProperty _comparisonValue;

    public EqualSpecification(TProperty comparisonValue)
    {
        _comparisonValue = comparisonValue;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) 
        => value is not null && value!.Equals(_comparisonValue);
    
    public override string GetSpecificationName() => "Equal";
    
    public override string GetErrorMessage() => "this is a test message";
}