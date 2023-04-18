using System.Linq.Expressions;
using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IRule<T, TProperty>
{
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TProperty>> Specifications { get; }

    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}