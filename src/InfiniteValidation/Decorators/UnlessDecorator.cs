namespace InfiniteValidation.Decorators;

public class UnlessDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly ISpecification<T, TProperty> _condition;

    public UnlessDecorator(ISpecification<T, TProperty> condition, ISpecification<T, TProperty> specification) : base(specification)
    {
        _condition = condition;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => _condition.IsSatisfiedBy(context, value) || Specification.IsSatisfiedBy(context, value);
}