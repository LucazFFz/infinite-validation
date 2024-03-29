using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal sealed class CollectionRule<T, TElement> : IValidatorRule<T>, IPropertyCollectionRule<T, TElement>
{
    private readonly Expression<Func<T, IEnumerable<TElement>>> _expression;
    
    public string PropertyName { get; set; }
    
    public Func<TElement, bool> ShouldValidateChildCondition { get; set; } = _ => true;
    
    public CascadeMode CascadeMode { get; set; }
    
    public List<IValidatorRule<TElement>> Rules { get; } = new();
    
    public List<ISpecification<T, TElement>> Specifications { get; } = new();
    
    public CollectionRule(Expression<Func<T, IEnumerable<TElement>>> expression, CascadeMode cascadeMode, string propertyName)
    { 
        CascadeMode = cascadeMode;
        _expression = expression;
        PropertyName = propertyName;
    }
    
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var instance = _expression.Compile()(context.InstanceToValidate);
        
        foreach (var element in instance.Where(element => ShouldValidateChildCondition.Invoke(element)))
            failures.AddRange(ValidateElement(context, element));
        
        return failures;
    }

    private IEnumerable<ValidationFailure> ValidateElement(ValidationContext<T> context, TElement property)
    {
        var failures = new List<ValidationFailure>();
        
        failures.AddRange(ValidateElementAgainstRules(new ValidationContext<TElement>(property, context.Settings)));
        failures.AddRange(ValidateElementAgainstSpecifications(context, property));
        
        return failures;
    }

    private IEnumerable<ValidationFailure> ValidateElementAgainstSpecifications(
        ValidationContext<T> context, 
        TElement property)
    {
        var failures = new List<ValidationFailure>();
        
        foreach (var specification in Specifications.Where(x => !x.IsSatisfiedBy(context, property)))
        {
            failures.Add(ValidationFailureFactory.Create(specification, property, PropertyName));
            if (CascadeMode == CascadeMode.Stop) break;
        }
        
        return failures;
    }
    
    private IEnumerable<ValidationFailure> ValidateElementAgainstRules(ValidationContext<TElement> context)
    {
        var failures = new List<ValidationFailure>();
        Rules.ForEach(rule => failures.AddRange(rule.Validate(context)));
        return failures;
    }
}