using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class Rule<T, TProperty> : IPropertyRule<T, TProperty>, IValidatorRule<T>
{
    public IValidator<TProperty>? ChildValidator { get; set; } 
    
    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();
    
    public Rule(Expression<Func<T, TProperty>> expression, CascadeMode cascadeMode)
    { 
        CascadeMode = cascadeMode;
        Expression = expression;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var property = Expression.Compile()(context.InstanceToValidate);

        if(ChildValidator != null) failures.AddRange(ChildValidator.Validate(property, context.Settings).Errors);
        
        foreach (var specification in Specifications
            .Where(specification => !specification.IsSatisfiedBy(context, property)))
        {
            failures.Add(ValidationFailureFactory.Create(specification, property));
            if (CascadeMode == CascadeMode.Stop) break;
        }

        return failures;
    }
}