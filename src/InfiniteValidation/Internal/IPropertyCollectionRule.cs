using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyCollectionRule<T, TElement>
{
    public IValidator<TElement> ChildValidator { get; set; }

    public Func<TElement, bool> ShouldValidateChildCondition { get; set; } 
    
    public Expression<Func<T, IEnumerable<TElement>>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TElement>> Specifications { get; }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}