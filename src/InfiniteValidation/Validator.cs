using System.Collections;
using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Internal;
using ValidationResult = InfiniteValidation.Results.ValidationResult;

namespace InfiniteValidation;

public abstract class Validator<T> : IValidator<T>
{
    private readonly List<IValidatorRule<T>> _rules = new();

    protected ValidatorConfiguration Configuration { get; } = new();
    
    public ValidationResult Validate(T instance, ValidationSettings settings) 
        => Validate(new ValidationContext<T>(instance), settings);
    
    public ValidationResult Validate(T instance) 
        => Validate(new ValidationContext<T>(instance), new ValidationSettings());

    public ValidationResult Validate(T instance, Action<ValidationSettings> settings)
    {
        var validationSettings = new ValidationSettings();
        settings.Invoke(validationSettings);
        return Validate(new ValidationContext<T>(instance), validationSettings);
    }

    public List<IValidatorRule<T>> GetRules() => _rules;
    
    protected IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var rule = new Rule<T, TProperty>(expression, Configuration.RuleLevelDefaultCascadeMode);
        var builder = new RuleBuilder<T, TProperty>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    protected ICollectionRuleBuilderInitial<T, TElement> RuleForEach<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression)
    {
        var rule = new CollectionRule<T, IEnumerable<TElement>, TElement>(expression, Configuration.RuleLevelDefaultCascadeMode);
        var builder = new CollectionRuleBuilder<T, IEnumerable<TElement>, TElement>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    protected void Include(IValidator<T> validator) =>  _rules.AddRange(validator.GetRules());

    private ValidationResult Validate(ValidationContext<T> context, ValidationSettings settings)
    {
        var result = new ValidationResult(settings);
        
        foreach (var rule in _rules)
        {
            result.Errors.AddRange(rule.IsValid(context));
            if (result.Errors.Any() && Configuration.ClassLevelDefaultCascadeMode == CascadeMode.Stop) break;
        }
        
        if (settings.ThrowExceptionOnInvalid && !result.IsValid) RaiseException(result);
        return result;
    }
    
    private static void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Errors);
}