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

    public static IRuleBuilderSettings<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex)
        => builder.AddSpecification(new RegularExpressionSpecification<T>(regex));
    
    public static IRuleBuilderSettings<T, string> Match<T>(this IRuleBuilder<T, string> builder, Regex regex)
        => builder.AddSpecification(new RegularExpressionSpecification<T>(regex));

    public static IRuleBuilderSettings<T, TProperty> When<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<TProperty, bool> condition)
        => builder.AddDecorator(new WhenDecorator<T, TProperty>(condition));
    
    public static IRuleBuilderSettings<T, TProperty> Unless<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<TProperty, bool> condition)
        => builder.AddDecorator(new UnlessDecorator<T, TProperty>(condition));

    public static IRuleBuilderSettings<T, TProperty> Otherwise<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, ISpecification<T, TProperty> specification)
        => builder.AddDecorator(new OtherwiseDecorator<T, TProperty>(specification));
    
    public static IRuleBuilderSettings<T, TProperty> Otherwise<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.AddDecorator(new OtherwiseDecorator<T, TProperty>(new PredicateSpecification<T, TProperty>(predicate)));
    
    public static IRuleBuilderSettings<T, TProperty> Not<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder)
        => builder.AddDecorator(new NotDecorator<T, TProperty>());
    
    public static IRuleBuilderSettings<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, string message)
        => builder.AddDecorator(new MessageDecorator<T, TProperty>(message));
    
    public static IRuleBuilderSettings<T, TProperty> WithSeverity<T, TProperty>(this IRuleBuilderSettings<T, TProperty> builder, Severity severity)
        => builder.AddDecorator(new SeverityDecorator<T, TProperty>(severity));
}