using InfiniteValidation.Internal;
using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public abstract class Decorator<T, TProperty> : IDecorator<T, TProperty>
{
    public ISpecification<T, TProperty> Specification { get; }

    public MessageBuilder MessageBuilder { get; }

    public Decorator(ISpecification<T, TProperty> specification)
    {
        specification.Guard(nameof(specification));
        Specification = specification;
        MessageBuilder = specification.MessageBuilder;
    }

    public virtual bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => Specification.IsSatisfiedBy(context, value);

    public virtual string GetName() => Specification.GetName();

    public virtual string GetMessageFormat() => Specification.GetMessageFormat();

    public virtual Severity GetSeverity() => Specification.GetSeverity();
}