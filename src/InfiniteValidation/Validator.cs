using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Internal;
using ValidationResult = InfiniteValidation.Results.ValidationResult;

namespace InfiniteValidation;

public abstract class Validator<T> : IValidator<T>
{
    private readonly List<IValidatorRule<T>> _rules = new();
    
    protected CascadeMode RuleLevelCascadeMode { get; set; } = CascadeMode.Continue;
    
    protected CascadeMode ClassLevelCascadeMode { get; set; } = CascadeMode.Continue;
    
    public ValidationResult Validate(T instance)
        => Validate(new ValidationContext<T>(instance), new ValidationOptions());
    
    public ValidationResult Validate(T instance, ValidationOptions options)
        => Validate(new ValidationContext<T>(instance), options);
    
    public List<IValidatorRule<T>> GetRules() => _rules;
    
    protected IRuleSettingsBuilder<T, TProperty> AddRule<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var rule = new PropertyRule<T, TProperty>(expression, RuleLevelCascadeMode);
        var builder = new RuleBuilder<T, TProperty>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    protected void Include(IValidator<T> validator) =>  _rules.AddRange(validator.GetRules());

    private ValidationResult Validate(ValidationContext<T> context, ValidationOptions options)
    {
        var result = new ValidationResult(options);
        
        foreach (var rule in _rules)
        {
            result.Errors.AddRange(rule.IsValid(context));
            if (result.Errors.Any() && ClassLevelCascadeMode == CascadeMode.Stop) break;
        }
        
        if (options.ThrowExceptionOnInvalid && !result.IsValid) RaiseException(result);
        return result;
    }
    
    private static void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Errors);
}