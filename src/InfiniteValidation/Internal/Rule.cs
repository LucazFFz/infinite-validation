using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class Rule<T, TProperty> : IValidatorRule<T>, IPropertyRule<T, TProperty>
{
    private readonly Expression<Func<T, TProperty>> _expression;
    
    public string PropertyName { get; set; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TProperty>> Specifications { get; set; } = new();

    public List<IValidatorRule<TProperty>> Rules { get; } = new();
    
    public Rule(Expression<Func<T, TProperty>> expression, CascadeMode cascadeMode, string propertyName)
    {
        CascadeMode = cascadeMode;
        _expression = expression;
        PropertyName = propertyName;
    }
    
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var property = _expression.Compile()(context.InstanceToValidate);
        
        failures.AddRange(ValidatePropertyAgainstSpecifications(context, property));
        failures.AddRange(ValidatePropertyAgainstRules(new ValidationContext<TProperty>(property, context.Settings)));
         
        return failures;
    }
    
    private IEnumerable<ValidationFailure> ValidatePropertyAgainstSpecifications(ValidationContext<T> context, TProperty property)
    {
        var failures = new List<ValidationFailure>();
        
        foreach (var specification in Specifications.Where(specification => !specification.IsSatisfiedBy(context, property)))
        {
            failures.Add(ValidationFailureFactory.Create(specification, property, PropertyName));
            if (CascadeMode == CascadeMode.Stop) break;
        }

        return failures;
    }
    
    private IEnumerable<ValidationFailure> ValidatePropertyAgainstRules(ValidationContext<TProperty> context)
    {
        var failures = new List<ValidationFailure>();
        Rules.ForEach(rule => failures.AddRange(rule.Validate(context)));
        return failures;
    }
}