using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Models;
using ValidationResult = InfiniteValidation.Models.ValidationResult;

namespace InfiniteValidation;

public abstract class AbstractValidator<T> : IValidator<T>
{
    private readonly List<IRule<T, dynamic>> _rules = new();
    
    public ValidationResult Validate(T instance)
        => Validate(new ValidationContext<T>(instance), new ValidationOptions());
    
    public ValidationResult Validate(T instance, ValidationOptions options)
        => Validate(new ValidationContext<T>(instance), options);
    
    protected IRuleBuilder<T, dynamic> AddRule(Expression<Func<T, dynamic>> expression)
    {
        var rule = new Rule<T, dynamic>(expression);
        var builder = new RuleBuilder<T, dynamic>(rule);
        _rules.Add(rule);
        return builder;
    }
    
    private ValidationResult Validate(ValidationContext<T> context, ValidationOptions options)
    {
        var result = new ValidationResult(options);
        
        _rules.ForEach(rule => result.Errors.AddRange(rule.IsValid(context)));

        if (options.ThrowExceptionOnInvalid && !result.IsValid)
            RaiseException(result);
        
        return result;
    }
    
    private void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Errors);
}