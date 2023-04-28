using InfiniteValidation.Internal;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public abstract class Decorator<T, TProperty> : IDecorator<T, TProperty>
{
    // TODO: find a workaround so this does not need to have a setter, required by RuleBuilder
    private ISpecification<T, TProperty> _specification = new DefaultSpecification<T, TProperty>();

    public ISpecification<T, TProperty> Specification
    {
        get => _specification;
        set 
        {
            value.Guard(nameof(value));
            _specification = value;
            MessageBuilder = value.MessageBuilder;
        }
    }

    public MessageBuilder MessageBuilder { get; private set; } = new();

    public virtual bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) 
        => _specification.IsSatisfiedBy(context, value);

    public virtual string GetSpecificationName() => _specification.GetSpecificationName();

    public virtual string GetMessageFormat() => _specification.GetMessageFormat();

    public virtual Severity GetSeverity() => _specification.GetSeverity();
}