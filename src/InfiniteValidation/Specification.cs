namespace InfiniteValidation;

public abstract class Specification<T, TProperty> : ISpecification<T, TProperty>
{
    public abstract bool IsSatisfiedBy(ValidationContext<T> context, TProperty value);

    public abstract string GetSpecificationName();
    
    public abstract string GetErrorMessage();

    public virtual Severity GetSeverity() => Severity.Error;
}