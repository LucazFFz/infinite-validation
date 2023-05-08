using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Internal;
using InfiniteValidation.Results;
using InfiniteValidation.RuleSetDecorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultExtensions
{
    public static IRuleSetBuilder<T> RuleSet<T>(this Validator<T> validator, string name, Action<IInlineValidator<T>> action)
    {
        var innerValidator = new InlineValidator<T>();
        action.Invoke(innerValidator);

        var rules = new List<IValidatorRule<T>>();
        innerValidator.GetRuleSets().ForEach(x => rules.AddRange(x.GetRules()));
        
        return validator.RuleSet(name, rules);
    }
    /*
    public static IRuleSetBuilder<T> Include<T>(this Validator<T> validator, Action<IInlineValidator<T>> action)
    {
        var innerValidator = new InlineValidator<T>();
        action.Invoke(innerValidator);
        
        innerValidator.GetRuleSets().ForEach(x => validator.RuleSet(x));
    }

    public static IRuleSetBuilder<T> Include<T>(this Validator<T> innerValidator, Validator<T> validator)
    {
        validator.GetRuleSets().ForEach(x => innerValidator.RuleSet(x));
    }
    */

    public static IRuleSetBuilder<T> IfTrue<T>(this IRuleSetBuilder<T> builder, Action<IInlineValidator<T>> action)
    {
        action.Guard(nameof(action));
        
        var rules = new List<IValidatorRule<T>>();
        var validator = new InlineValidator<T>();
        
        action.Invoke(validator);
        validator.GetRuleSets().ForEach(x => rules.AddRange(x.GetRules()));
        
        return builder.Decorate(new RuleSetIfTrueDecorator<T>(new RuleSet<T>(Validator<T>.DefaultRuleSetName, rules)));
    }

    public static IRuleBuilderSettings<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.AddSpecification(new PredicateSpecification<T, TProperty>(predicate));
    
    public static IRuleBuilderSettings<T, TProperty> Null<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.AddSpecification(new NullSpecification<T, TProperty>());

    public static IRuleBuilderSettings<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value)
        => builder.AddSpecification(new EqualSpecification<T, TProperty>(value));

    public static IRuleBuilderSettings<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value, IEqualityComparer<TProperty> comparer)
        => builder.AddSpecification(new EqualSpecification<T, TProperty>(value, comparer));

    public static IRuleBuilderSettings<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex)
        => builder.AddSpecification(new RegexSpecification<T>(regex));
    
    public static IRuleBuilderSettings<T, string> Match<T>(this IRuleBuilder<T, string> builder, Regex regex)
        => builder.AddSpecification(new RegexSpecification<T>(regex));
    
    public static IRuleBuilderSettings<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex, RegexOptions options)
        => builder.AddSpecification(new RegexSpecification<T>(regex, options));

    public static IRuleBuilderSettings<T, TProperty> When<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<T, bool> condition)
        => builder.Decorate(new WhenDecorator<T, TProperty>(condition));
    
    public static IRuleBuilderSettings<T, TProperty> Unless<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<T, bool> condition)
        => builder.Decorate(new UnlessDecorator<T, TProperty>(condition));

    public static IRuleBuilderSettings<T, TProperty> Not<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder)
        => builder.Decorate(new NotDecorator<T, TProperty>());
    
    public static IRuleBuilderSettings<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, string message)
        => builder.Decorate(new MessageDecorator<T, TProperty>(message));
    
    public static IRuleBuilderSettings<T, TProperty> WithSeverity<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Severity severity)
        => builder.Decorate(new SeverityDecorator<T, TProperty>(severity));
    
    public static IRuleBuilderSettings<T, TProperty> WithSpecificationName<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, string specificationName)
        => builder.Decorate(new SpecificationNameDecorator<T, TProperty>(specificationName));
}