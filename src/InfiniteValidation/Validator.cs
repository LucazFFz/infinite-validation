using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Internal;
using InfiniteValidation.Models;
using ValidationResult = InfiniteValidation.Models.ValidationResult;

namespace InfiniteValidation;

public abstract class Validator<T> : IValidator<T>
{
    private readonly List<IRule<T, dynamic>> _rules = new();

    protected ValidationOptions ValidationOptions { get; private set; } = new();

    public ValidationResult Validate(T instance)
        => Validate(new ValidationContext<T>(instance), new ValidationOptions());
    
    public ValidationResult Validate(T instance, ValidationOptions options)
        => Validate(new ValidationContext<T>(instance), options);
    
    public List<IRule<T, dynamic>> GetRules() => _rules;
    
    protected IRuleSettingsBuilder<T, dynamic> AddRule(Expression<Func<T, dynamic>> expression)
    {
        var rule = new Rule<T, dynamic>(expression, ValidationOptions.RuleLevelDefaultCascadeMode);
        var builder = new RuleBuilder<T, dynamic>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    protected void Include(IValidator<T> validator) =>  _rules.AddRange(validator.GetRules());

    private ValidationResult Validate(ValidationContext<T> context, ValidationOptions options)
    {
        ValidationOptions = options;
        var result = new ValidationResult(options);
        _rules.ForEach(rule => result.Errors.AddRange(rule.IsValid(context)));

        if (options.ThrowExceptionOnInvalid && !result.IsValid)
            RaiseException(result);
        
        return result;
    }
    
    private static void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Errors);
}