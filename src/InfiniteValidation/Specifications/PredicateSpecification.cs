using InfiniteValidation.Internal;

namespace InfiniteValidation.Specifications;

public class PredicateSpecification<T, TProperty> : Specification<T, TProperty>
{
    private readonly Func<TProperty, bool> _predicate;

    public PredicateSpecification(Func<TProperty, bool> predicate)
    {
        predicate.Guard(nameof(predicate));
        _predicate = predicate;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => _predicate.Invoke(value);

    public override string GetSpecificationName() => "PredicateSpecification";

    public override string GetErrorMessage() => "Value does not pass predicate";
}