using InfiniteValidation.Specifications;

namespace InfiniteValidation.Internal;

internal class SpecificationBuilder<T, TProperty>
{
    private ISpecification<T, TProperty> _specification;

    public SpecificationBuilder(ISpecification<T, TProperty> specification)
    {
        specification.Guard(nameof(specification));
        _specification = specification;
    }

    public SpecificationBuilder<T, TProperty> Decorate(IDecorator<T, TProperty> decorator)
    {
        _specification = decorator;
        return this;
    }

    internal ISpecification<T, TProperty> Build() => _specification;
}