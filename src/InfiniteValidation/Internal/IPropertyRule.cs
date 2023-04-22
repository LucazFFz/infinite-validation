using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyRule<T, TProperty> : IValidatorRule<T>
{
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TProperty>> Specifications { get; }
    
    public new IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}