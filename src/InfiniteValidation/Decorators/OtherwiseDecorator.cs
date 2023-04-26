using InfiniteValidation.Internal;

namespace InfiniteValidation.Decorators;

public class OtherwiseDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly ISpecification<T, TProperty> _specification;

    public OtherwiseDecorator(ISpecification<T, TProperty> specification)
    {
        specification.Guard(nameof(specification));
        _specification = specification;
    }

    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => Specification.IsSatisfiedBy(context, value) || _specification.IsSatisfiedBy(context, value);
    
    public override string GetSpecificationName() => _specification.GetSpecificationName();

    public override string GetErrorMessage() => _specification.GetErrorMessage();

    public override Severity GetSeverity() => _specification.GetSeverity();
}