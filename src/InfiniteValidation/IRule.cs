using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IRule<T, TProperty>
{
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);

    public void AddSpecification(ISpecification<T, TProperty> specification);

    public List<ISpecification<T, TProperty>> GetSpecifications();
}