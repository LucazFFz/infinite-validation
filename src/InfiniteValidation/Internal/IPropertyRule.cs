using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyRule<T, TProperty>
{
    public string PropertyName { get; set; }
    
    public IValidator<TProperty>? ChildValidator { get; set; } 
    
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TProperty>> Specifications { get; }
    
    public IEnumerable<SpecificationFailure> IsValid(ValidationContext<T> context);
}