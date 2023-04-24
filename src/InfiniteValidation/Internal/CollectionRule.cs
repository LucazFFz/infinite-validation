using System.Collections;
using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

public class CollectionRule<T, TProperty, TElement> : IPropertyCollectionRule<T, TProperty, TElement>, IValidatorRule<T> 
    where TProperty : IEnumerable<TElement>
{
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TElement>> Specifications { get; set; } = new();
    
    public CollectionRule(Expression<Func<T, TProperty>> expression, CascadeMode cascadeMode)
    { 
        CascadeMode = cascadeMode;
        Expression = expression;
    }

    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var value = Expression.Compile()(context.InstanceToValidate);

        foreach (var specification in Specifications)
        {
            foreach (var property in value)
            {
                if (specification.IsSatisfiedBy(context, property)) continue;
                failures.Add(ValidationFailureFactory.Create(specification, property));
                if (CascadeMode == CascadeMode.Stop) break;
            }
        }

        return failures;
    }
}