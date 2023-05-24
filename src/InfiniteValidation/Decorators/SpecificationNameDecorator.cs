namespace InfiniteValidation.Decorators;

public class SpecificationNameDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly string _specificationName;

    public SpecificationNameDecorator(ISpecification<T, TProperty> specification, string specificationName) : base(specification)
    {
        _specificationName = specificationName;
    }

    public override string GetName() => _specificationName;
}