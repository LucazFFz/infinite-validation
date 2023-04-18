using System.Linq.Expressions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Internal;

internal class Rule<T, TProperty> : IRule<T, TProperty>
{
    private readonly Expression<Func<T, TProperty>> _expression;
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();
    
    public Rule(Expression<Func<T, TProperty>> expression, Models.CascadeMode cascadeMode)
    { 
        CascadeMode = cascadeMode;
        _expression = expression;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var value = _expression.Compile()(context.InstanceToValidate);

        foreach (var specification in Specifications.Where(specification => !specification.IsSatisfiedBy(context, value)))
        {
            failures.Add(specification.GetValidationFailure(value));
            if (CascadeMode == CascadeMode.Stop) return failures;
        }

        return failures;
    }
}