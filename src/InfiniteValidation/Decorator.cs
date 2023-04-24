namespace InfiniteValidation;

public abstract class Decorator<T, TProperty> : IDecorator<T, TProperty>
{
    // TODO: find a workaround so this does not need to have a setter, required by RuleBuilder
    public ISpecification<T, TProperty> Specification { get; set; } = Specification<T, TProperty>.Default();

    public virtual bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) 
        => Specification.IsSatisfiedBy(context, value);

    public virtual string GetSpecificationName() => Specification.GetSpecificationName();

    public virtual string GetErrorMessage() => Specification.GetErrorMessage();

    public virtual Severity GetSeverity() => Specification.GetSeverity();
}