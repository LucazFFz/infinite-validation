using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public static class DefaultDecoratorExtensions
{
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