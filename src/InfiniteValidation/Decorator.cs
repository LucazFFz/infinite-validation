using InfiniteValidation.Results;

namespace InfiniteValidation;

public abstract class Decorator<T, TProperty> : ISpecification<T, TProperty>
{
    protected ISpecification<T, TProperty> Specification { get; }
    
    protected Decorator(ISpecification<T, TProperty> specification)
    {
        Specification = specification;
    }

    public virtual bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => Specification.IsSatisfiedBy(context, value);

    public virtual string GetSpecificationName() => Specification.GetSpecificationName();

    public virtual string GetErrorMessage() => Specification.GetErrorMessage();

    public virtual Severity GetSeverity() => Specification.GetSeverity();

    public ValidationFailure GetValidationFailure(TProperty value)
    {
        if (value is null) throw new ArgumentNullException(GetSpecificationName());

        return new ValidationFailure()
        {
            FailureMessage = GetErrorMessage(),
            SpecificationName = GetSpecificationName(),
            Severity = GetSeverity(),
            AttemptedValue = value
        };
    }
}