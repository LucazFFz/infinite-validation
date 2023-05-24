namespace InfiniteValidation.Specifications;

public class EqualSpecification<T, TProperty> : Specification<T, TProperty>
{
    private readonly TProperty _comparisonValue;

    private readonly IEqualityComparer<TProperty>? _comparer;

    public EqualSpecification(TProperty comparisonValue, IEqualityComparer<TProperty>? comparer = null)
    {
        _comparisonValue = comparisonValue;
        _comparer = comparer;
        
        MessageBuilder.Append("ComparisonValue", comparisonValue);
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) 
        => Compare(_comparisonValue, value); 
    
    public override string GetName() => "EqualSpecification";
    
    public override string GetMessageFormat() => "'{PropertyName}' must equal '{ComparisonValue}'.";

    private bool Compare(TProperty comparisonValue, TProperty value) 
        => _comparer?.Equals(comparisonValue, value) ?? Equals(_comparisonValue, value);
}