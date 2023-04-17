using InfiniteValidation.Models;

namespace InfiniteValidation;

public abstract class AbstractDecorator<T, TProperty> : ISpecification<T, TProperty>
{
    protected ISpecification<T, TProperty> Specification { get; }
    
    protected AbstractDecorator(ISpecification<T, TProperty> specification)
    {
        Specification = specification;
    }

    public virtual bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => Specification.IsSatisfiedBy(context, value);

    public virtual string GetSpecificationName() => Specification.GetSpecificationName();

    public virtual string GetErrorMessage() => Specification.GetErrorMessage();

    public virtual Severity GetSeverity() => Specification.GetSeverity();

    public ValidationFailure GetValidationFailure(TProperty value) => Specification.GetValidationFailure(value);
}