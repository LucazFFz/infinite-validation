using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultExtensions
{
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

    public static IRuleBuilderSettings<T, TProperty> When<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<TProperty, bool> condition)
        => builder.Decorate(new WhenDecorator<T, TProperty>(condition));
    
    public static IRuleBuilderSettings<T, TProperty> Unless<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<TProperty, bool> condition)
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