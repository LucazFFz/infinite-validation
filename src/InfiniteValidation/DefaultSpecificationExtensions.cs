using System.Text.RegularExpressions;
using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultSpecificationExtensions
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
}