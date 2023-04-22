using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class PropertyRule<T, TProperty> : IPropertyRule<T, TProperty>
{
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();
    
    public PropertyRule(Expression<Func<T, TProperty>> expression, CascadeMode cascadeMode)
    { 
        CascadeMode = cascadeMode;
        Expression = expression;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var value = Expression.Compile()(context.InstanceToValidate);

        foreach (var specification in Specifications
            .Where(specification => !specification.IsSatisfiedBy(context, value)))
        {
            failures.Add(ValidationFailureFactory.Create(specification, value));
            if (CascadeMode == CascadeMode.Stop) break;
        }

        return failures;
    }
}