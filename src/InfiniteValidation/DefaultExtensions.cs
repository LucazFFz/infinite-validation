using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultExtensions
{
    public static IRuleBuilder<T, TProperty> Must<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate)
        => builder.Must(new PredicateSpecification<T, TProperty>(predicate));
    
    public static IRuleBuilder<T, TProperty> Null<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.Must(new NullSpecification<T, TProperty>());

    public static IRuleBuilder<T, TProperty> NotNull<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
        => builder.Must(new NotDecorator<T, TProperty>(new NullSpecification<T, TProperty>()));

    public static IRuleBuilder<T, TProperty> Equal<T, TProperty>(this IRuleBuilder<T, TProperty> builder, TProperty value)
        => builder.Must(new EqualSpecification<T, TProperty>(value));

    public static IRuleBuilder<T, string> Match<T>(this IRuleBuilder<T, string> builder, string regex)
        => builder.Must(new RegularExpressionSpecification<T>(regex));
    
    public static IRuleBuilder<T, string> Match<T>(this IRuleBuilder<T, string> builder, Regex regex)
        => builder.Must(new RegularExpressionSpecification<T>(regex));
    
    public static IRuleBuilder<T, TProperty> When<T, TProperty>(this IRuleBuilder<T, TProperty> builder, ISpecification<T, TProperty> condition, ISpecification<T, TProperty> specification)
        => builder.Must(new WhenDecorator<T, TProperty>(condition, specification));
    
    public static IRuleBuilder<T, TProperty> When<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> predicate, ISpecification<T, TProperty> specification)
        => builder.Must(new WhenDecorator<T, TProperty>(new PredicateSpecification<T, TProperty>(predicate), specification));

    public static IRuleBuilder<T, TProperty> Unless<T, TProperty>(this IRuleBuilder<T, TProperty> builder, ISpecification<T, TProperty> condition, ISpecification<T, TProperty> specification)
        => builder.Must(new UnlessDecorator<T, TProperty>(condition, specification));
    
    public static IRuleBuilder<T, TProperty> Unless<T, TProperty>(this IRuleBuilder<T, TProperty> builder, Func<TProperty, bool> condition, ISpecification<T, TProperty> specification)
        => builder.Must(new UnlessDecorator<T, TProperty>(new PredicateSpecification<T, TProperty>(condition), specification));
    
    public static IRuleBuilder<T, TProperty> Not<T, TProperty>(this IRuleBuilder<T, TProperty> builder, ISpecification<T, TProperty> specification)
        => builder.Must(new NotDecorator<T, TProperty>(specification));

    public static IRuleBuilder<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> builder, ISpecification<T, TProperty> specification, string message)
        => builder.Must(new MessageDecorator<T,TProperty>(specification, message));
    
    public static IRuleBuilder<T, TProperty> WithSeverity<T, TProperty>(this IRuleBuilder<T, TProperty> builder, ISpecification<T, TProperty> specification, Severity severity)
        => builder.Must(new SeverityDecorator<T, TProperty>(specification, severity));
}