namespace InfiniteValidation.Decorators;

public class NotDecorator<T, TProperty> : Decorator<T, TProperty>
{
    public NotDecorator() {}
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => !Specification.IsSatisfiedBy(context, value);
    
}