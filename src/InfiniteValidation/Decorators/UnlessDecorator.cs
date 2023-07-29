using InfiniteValidation.Internal;

namespace InfiniteValidation.Decorators;

public sealed class UnlessDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Func<T, bool> _condition;

    public UnlessDecorator(ISpecification<T, TProperty> specification,  Func<T, bool> condition) : base(specification)
    {
        condition.Guard(nameof(condition));
        _condition = condition;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => _condition.Invoke(context.InstanceToValidate) || Specification.IsSatisfiedBy(context, value);
}