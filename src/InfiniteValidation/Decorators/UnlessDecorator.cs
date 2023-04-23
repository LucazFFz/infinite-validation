namespace InfiniteValidation.Decorators;

public class UnlessDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Func<TProperty, bool> _condition;

    public UnlessDecorator(ISpecification<T, TProperty> specification, Func<TProperty, bool> condition) : base(specification)
    {
        _condition = condition;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => _condition.Invoke(value) || Specification.IsSatisfiedBy(context, value);
}