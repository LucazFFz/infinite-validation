using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultExtensions
{
    public static IRuleBuilder<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.AddSpecification(new PredicateSpecification<T, TProperty>(predicate));
    
    public static IRuleBuilder<T, TProperty> Null<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.AddSpecification(new NullSpecification<T, TProperty>());

    public static IRuleBuilder<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value)
        => builder.AddSpecification(new EqualSpecification<T, TProperty>(value));

    public static IRuleBuilder<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex)
        => builder.AddSpecification(new RegularExpressionSpecification<T>(regex));
    
    public static IRuleBuilder<T, string> Match<T>(this IRuleBuilder<T, string> builder, Regex regex)
        => builder.AddSpecification(new RegularExpressionSpecification<T>(regex));

    public static IRuleBuilder<T, TProperty> When<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.AddDecorator(new WhenDecorator<T, TProperty>(predicate));
    
    public static IRuleBuilder<T, TProperty> Unless<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.AddDecorator(new UnlessDecorator<T, TProperty>(predicate));
    
    public static IRuleBuilder<T, TProperty> Not<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.AddDecorator(new NotDecorator<T, TProperty>());
    
    public static IRuleBuilder<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> builder, string message)
        => builder.AddDecorator(new MessageDecorator<T, TProperty>(message));
    
    public static IRuleBuilder<T, TProperty> WithSeverity<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Severity severity)
        => builder.AddDecorator(new SeverityDecorator<T, TProperty>(severity));

}