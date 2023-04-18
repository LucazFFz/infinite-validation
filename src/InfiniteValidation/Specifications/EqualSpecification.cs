using InfiniteValidation.Extensions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class EqualSpecification<T, TProperty> : Specification<T, TProperty>
{ 
    private readonly TProperty _comparisonValue;

    private readonly IEqualityComparer<TProperty>? _comparer;

    public EqualSpecification(TProperty comparisonValue, IEqualityComparer<TProperty>? comparer = null)
    {
        _comparisonValue = comparisonValue;
        _comparer = comparer;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) 
        => Compare(_comparisonValue, value); 
    
    public override string GetSpecificationName() => "EqualSpecification";
    
    public override string GetErrorMessage() => $"Value is not equal to {_comparisonValue}";

    private bool Compare(TProperty comparisonValue, TProperty value) 
        => _comparer?.Equals(comparisonValue, value) ?? Equals(_comparisonValue, value);
}