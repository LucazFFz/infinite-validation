using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface ISpecification<T, in TProperty>
{
    public bool IsSatisfiedBy(ValidationContext<T> context, TProperty value);

    public string GetSpecificationName();

    public string GetErrorMessage();

    public Severity GetSeverity();
}