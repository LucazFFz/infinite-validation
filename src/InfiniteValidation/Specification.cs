using InfiniteValidation.Specifications;

namespace InfiniteValidation;

public abstract class Specification<T, TProperty> : ISpecification<T, TProperty>
{
    public MessageBuilder MessageBuilder => new();
    
    public abstract bool IsSatisfiedBy(ValidationContext<T> context, TProperty value);

    public abstract string GetSpecificationName();
    
    public abstract string GetMessageFormat();

    public virtual Severity GetSeverity() => Severity.Error;

    public static ISpecification<T, TProperty> Default() => new DefaultSpecification<T, TProperty>();
}