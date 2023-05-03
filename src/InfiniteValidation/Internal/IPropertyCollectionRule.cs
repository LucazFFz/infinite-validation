using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyCollectionRule<T, TElement>
{
    public string PropertyName { get; set; }
    
    public IValidator<TElement> ChildValidator { get; set; }

    public Func<TElement, bool> ShouldValidateChildCondition { get; set; } 
    
    public Expression<Func<T, IEnumerable<TElement>>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TElement>> Specifications { get; }
    
    public IEnumerable<SpecificationFailure> IsValid(ValidationContext<T> context);
}