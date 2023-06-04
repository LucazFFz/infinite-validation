using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyRule<T, TProperty>
{
    public string PropertyName { get; set; }

    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; }

    public List<IValidatorRule<TProperty>> Rules { get; }
    
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context);
}