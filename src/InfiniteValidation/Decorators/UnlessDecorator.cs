using InfiniteValidation.Internal;

namespace InfiniteValidation.Decorators;

public class UnlessDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Func<T, bool> _condition;

    public UnlessDecorator(Func<T, bool> condition)
    {
        condition.Guard(nameof(condition));
        _condition = condition;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => _condition.Invoke(context.InstanceToValidate) || Specification.IsSatisfiedBy(context, value);
}