using InfiniteValidation.Internal;

namespace InfiniteValidation.Decorators;

public class WhenDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Func<TProperty, bool> _condition;

    public WhenDecorator(Func<TProperty, bool> condition)
    {
        condition.Guard(nameof(condition));
        _condition = condition;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => !_condition.Invoke(value) || Specification.IsSatisfiedBy(context, value);
}