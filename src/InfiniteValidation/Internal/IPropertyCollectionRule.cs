using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyCollectionRule<T, TElement>
{
    public string PropertyName { get; set; }

    public Func<TElement, bool> ShouldValidateChildCondition { get; set; }

    public CascadeMode CascadeMode { get; set; }
    
    public List<ISpecification<T, TElement>> Specifications { get; }
    
    public List<IRuleset<TElement>> Rulesets { get; }
    
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context);
}