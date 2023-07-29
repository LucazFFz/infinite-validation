using InfiniteValidation.Internal;

namespace InfiniteValidation.Specifications;

public sealed class PredicateSpecification<T, TProperty> : Specification<T, TProperty>
{
    private readonly Func<TProperty, bool> _predicate;

    public PredicateSpecification(Func<TProperty, bool> predicate)
    {
        predicate.Guard(nameof(predicate));
        _predicate = predicate;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => _predicate.Invoke(value);

    public override string GetName() => "PredicateSpecification";

    public override string GetMessageFormat() => "'{PropertyName}' must fulfill predicate.";
}