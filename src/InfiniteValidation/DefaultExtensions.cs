using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultExtensions
{
    public static IRuleBuilderDecorator<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.Specify(new PredicateSpecification<T, TProperty>(predicate));
    
    public static IRuleBuilderDecorator<T, TProperty> Null<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.Specify(new NullSpecification<T, TProperty>());

    public static IRuleBuilderDecorator<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value)
        => builder.Specify(new EqualSpecification<T, TProperty>(value));

    public static IRuleBuilderDecorator<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value, IEqualityComparer<TProperty> comparer)
        => builder.Specify(new EqualSpecification<T, TProperty>(value, comparer));

    public static IRuleBuilderDecorator<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex)
        => builder.Specify(new RegexSpecification<T>(regex));

    public static IRuleBuilderDecorator<T, string> Match<T>(this IRuleBuilder<T, string> builder, Regex regex)
    {
        return builder.Specify(new RegexSpecification<T>(regex));
    }

    public static IRuleBuilderDecorator<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex, RegexOptions options)
        => builder.Specify(new RegexSpecification<T>(regex, options));

    public static IRuleBuilderDecorator<T, TProperty> When<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder, Func<T, bool> condition)
        => builder.Decorate(new WhenDecorator<T, TProperty>(builder.GetSpecificationToDecorate(), condition));
    
    public static IRuleBuilderDecorator<T, TProperty> Unless<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder, Func<T, bool> condition)
        => builder.Decorate(new UnlessDecorator<T, TProperty>(builder.GetSpecificationToDecorate(), condition));

    public static IRuleBuilderDecorator<T, TProperty> Not<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder)
        => builder.Decorate(new NotDecorator<T, TProperty>(builder.GetSpecificationToDecorate()));
    
    public static IRuleBuilderDecorator<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder, string message)
        => builder.Decorate(new MessageDecorator<T, TProperty>(builder.GetSpecificationToDecorate(), message));
    
    public static IRuleBuilderDecorator<T, TProperty> WithSeverity<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder, Severity severity)
        => builder.Decorate(new SeverityDecorator<T, TProperty>(builder.GetSpecificationToDecorate(), severity));
    
    public static IRuleBuilderDecorator<T, TProperty> WithSpecificationName<T, TProperty>(this IRuleBuilderDecorator<T, TProperty> builder, string specificationName)
        => builder.Decorate(new SpecificationNameDecorator<T, TProperty>(builder.GetSpecificationToDecorate(), specificationName));
}