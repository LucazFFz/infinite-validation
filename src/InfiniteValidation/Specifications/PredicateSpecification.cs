using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class PredicateSpecification<T, TProperty> : Specification<T, TProperty>
{
    private readonly Func<TProperty, bool> _predicate;

    public PredicateSpecification(Func<TProperty, bool> predicate)
    {
        _predicate = predicate;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => value is not null && _predicate.Invoke(value);
    
    public override string GetSpecificationName() => "Predicate";
    
    public override string GetErrorMessage() => "this is a test message";
}