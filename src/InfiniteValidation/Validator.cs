using System.Linq.Expressions;
using InfiniteValidation.Exceptions;
using InfiniteValidation.Internal;
using ValidationResult = InfiniteValidation.Results.ValidationResult;

namespace InfiniteValidation;

public abstract class Validator<T> : IValidator<T>
{
    public const string DefaultRuleSetName = "Default";
    
    private readonly List<RuleSetBuilder<T>> _ruleSetBuilders = new();

    public ValidatorConfiguration Configuration { get; } = new();

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

    public List<IRuleSet<T>> GetRuleSets()
    {
        var ruleSets = new List<IRuleSet<T>>();
        _ruleSetBuilders.ForEach(x => ruleSets.Add(x.Build()));
        return ruleSets;
    }

    public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var rule = new Rule<T, TProperty>(expression, Configuration.RuleLevelDefaultCascadeMode, GetPropertyName(expression));
        var builder = new RuleBuilder<T, TProperty>(rule);
        RuleSet(DefaultRuleSetName, new List<IValidatorRule<T>> { rule });
        return builder;
    }
    
    public ICollectionRuleBuilderInitial<T, TElement> RuleForEach<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression)
    {
        var rule = new CollectionRule<T, TElement>(expression, Configuration.RuleLevelDefaultCascadeMode, GetPropertyName(expression));
        var builder = new CollectionRuleBuilder<T, TElement>(rule);
        RuleSet(DefaultRuleSetName, new List<IValidatorRule<T>> { rule });
        return builder;
    }

    public IRuleSetBuilder<T> RuleSet(string name, IEnumerable<IValidatorRule<T>> rules)
    {
        rules.Guard(nameof(rules));
        return RuleSet(new RuleSet<T>(name, rules));
    }
    
    private ValidationResult Validate(ValidationContext<T> context)
    {
        var result = new ValidationResult(context.Settings);

        foreach (var ruleSet in _ruleSetBuilders.Select(ruleSetBuilder => ruleSetBuilder.Build()))
        {
            if (context.Settings.RuleSetsToValidate.Contains(ruleSet.GetName()) || ruleSet.GetName() == DefaultRuleSetName) 
                result.Failures.AddRange(ruleSet.IsValid(context));
            if (result.Failures.Any() && Configuration.ClassLevelDefaultCascadeMode == CascadeMode.Stop) break;
        }
        
        if (context.Settings.ThrowExceptionOnInvalid && !result.IsValid) RaiseException(result);
        return result;
    }
    
    private static void RaiseException(ValidationResult result)
        => throw new ValidationException(result.Failures);

    private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var action = (MemberExpression) expression.Body;
        return action.Member.Name;
    }
    
    private IRuleSetBuilder<T> RuleSet(IRuleSet<T> ruleSet)
    {
        var builder = new RuleSetBuilder<T>(ruleSet);
        _ruleSetBuilders.Add(builder);
        return builder;
    }
}