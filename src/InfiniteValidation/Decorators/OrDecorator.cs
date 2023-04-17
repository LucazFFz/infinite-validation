using InfiniteValidation.Models;

namespace InfiniteValidation.Decorators;

public class OrDecorator<T, TProperty> : AbstractDecorator<T, TProperty>
{
    private readonly ISpecification<T, TProperty> _leftSpecification;
    private readonly ISpecification<T, TProperty> _rightSpecification;
    
    public OrDecorator(ISpecification<T, TProperty> leftSpecification, ISpecification<T, TProperty> rightSpecification) : base(null)
    {
        _leftSpecification = leftSpecification;
        _rightSpecification = rightSpecification;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => _leftSpecification.IsSatisfiedBy(context, value) || _rightSpecification.IsSatisfiedBy(context, value);

    public override string GetSpecificationName() 
        => $"{_leftSpecification.GetSpecificationName()}, {_rightSpecification.GetSpecificationName()}";
}