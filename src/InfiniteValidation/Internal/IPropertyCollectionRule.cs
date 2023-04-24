using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyCollectionRule<T, TProperty, TElement>
{
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TElement>> Specifications { get; }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}