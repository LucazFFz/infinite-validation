namespace InfiniteValidation.Specifications;

public class DefaultSpecification<T, TProperty> : Specification<T, TProperty>
{
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => true;

    public override string GetName() => "DefaultSpecification";

    public override string GetMessageFormat() => string.Empty;
}