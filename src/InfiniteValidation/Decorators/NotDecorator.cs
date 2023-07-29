namespace InfiniteValidation.Decorators;

public sealed class NotDecorator<T, TProperty> : Decorator<T, TProperty>
{
    public NotDecorator(ISpecification<T, TProperty> specification) : base(specification)
    {
        
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => !Specification.IsSatisfiedBy(context, value);
    
}