using InfiniteValidation.Models;

namespace InfiniteValidation;

public abstract class AbstractSpecification<T, TProperty> : ISpecification<T, TProperty>
{
    public abstract bool IsSatisfiedBy(ValidationContext<T> context, TProperty value);

    public abstract string GetSpecificationName();
    
    public abstract string GetErrorMessage();

    public virtual Severity GetSeverity() => Severity.Error;

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