using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class Rule<T, TProperty> : IPropertyRule<T, TProperty>, IValidatorRule<T>
{
    public string PropertyName { get; set; }

    public Expression<Func<T, TProperty>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();
    
    public Rule(Expression<Func<T, TProperty>> expression, CascadeMode cascadeMode, string propertyName)
    {
        CascadeMode = cascadeMode;
        Expression = expression;
        PropertyName = propertyName;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var property = Expression.Compile()(context.InstanceToValidate);

        foreach (var specification in Specifications
                     .Where(specification => !specification.IsSatisfiedBy(context, property)))
        {
            specification.MessageBuilder.AppendPropertyName(PropertyName).AppendAttemptedValue(property);
            failures.Add(ValidationFailureFactory.Create(specification, property));
            if (CascadeMode == CascadeMode.Stop) break;
        }

        return failures;
    }
}