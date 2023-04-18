using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IRule<T, TProperty>
{
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TProperty>> Specifications { get; }

    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}