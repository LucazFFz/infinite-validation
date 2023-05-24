using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Internal;
using ValidationResult = InfiniteValidation.Results.ValidationResult;

namespace InfiniteValidation;

public abstract class Validator<T> : IValidator<T>
{
    public const string DefaultRulesetKey = "Default";
    
    private readonly List<IRuleset<T>> _rulesets = new();

    public ValidatorConfiguration Configuration { get; } = new();

    public void Ruleset(string key, Action<IInlineValidator<T>> action)
    {
        var validator = new InlineValidator<T>();
        action.Invoke(validator);

        var rules = new List<IValidatorRule<T>>();
        validator.GetRulesets().ForEach(x => rules.AddRange(x.GetRules()));
        
        _rulesets.Add(new Ruleset<T>(key, rules, Configuration.ClassLevelDefaultCascadeMode));
    }

    public void Include(Validator<T> validator) => validator.GetRulesets().ForEach(x => _rulesets.Add(x));
    
    public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var rule = new Rule<T, TProperty>(
            expression, 
            Configuration.RuleLevelDefaultCascadeMode, 
            GetPropertyName(expression));
        
        _rulesets.Add(new Ruleset<T>(
            DefaultRulesetKey, 
            new List<IValidatorRule<T>> { rule }, 
            Configuration.ClassLevelDefaultCascadeMode));
        
        return new RuleBuilder<T, TProperty>(rule);
    }
    
    public ICollectionRuleBuilderInitial<T, TElement> RuleForEach<TElement>(
        Expression<Func<T, IEnumerable<TElement>>> expression)
    {
        var rule = new CollectionRule<T, TElement>(
            expression, 
            Configuration.RuleLevelDefaultCascadeMode, 
            GetPropertyName(expression));
        
        _rulesets.Add(new Ruleset<T>(
            DefaultRulesetKey, 
            new List<IValidatorRule<T>> { rule }, 
            Configuration.ClassLevelDefaultCascadeMode));
        
        return new CollectionRuleBuilder<T, TElement>(rule);
    }

    public List<IRuleset<T>> GetRulesets() => _rulesets;

    public ValidationResult Validate(T instance, ValidationSettings settings)
        => Validate(new ValidationContext<T>(instance, settings));

    public ValidationResult Validate(T instance) 
        => Validate(new ValidationContext<T>(instance, new ValidationSettings()));

    public ValidationResult Validate(T instance, Action<ValidationSettings> settings)
    {
        var validationSettings = new ValidationSettings();
        settings.Invoke(validationSettings);
        return Validate(new ValidationContext<T>(instance, validationSettings));
    }

    private ValidationResult Validate(ValidationContext<T> context)
    {
        var result = new ValidationResult(context.Settings);

        foreach (var ruleset in _rulesets)
        {
            if (context.Settings.ShouldValidateRuleset(ruleset)) 
                result.Failures.AddRange(ruleset.Validate(context));
            
            if (result.UnconditionalIsValid && Configuration.ClassLevelDefaultCascadeMode == CascadeMode.Stop) break;
        }
        
        if (!result.IsValid && context.Settings.ThrowExceptionOnInvalid) RaiseException(result);
        return result;
    }

    private static void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Failures);

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        try
        {
            var action = (MemberExpression)expression.Body;
            return action.Member.Name;
        }
        catch (Exception e)
        {
            return "Property";
        }
    }
}