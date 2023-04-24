namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleBuilderInitial<T, TProperty>, IRuleBuilderSettings<T, TProperty>
{
    private readonly IPropertyRule<T, TProperty> _propertyRule;

    public RuleBuilder(IPropertyRule<T, TProperty> propertyRule)
    {
        _propertyRule = propertyRule;
    }

    public IRuleBuilderInitial<T, TProperty> CascadeMode(CascadeMode mode)
    {
        _propertyRule.CascadeMode = mode;
        return this;
    }
    
    public IRuleBuilderSettings<T, TProperty> AddSpecification(ISpecification<T, TProperty> specification)
    {
        _propertyRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderSettings<T, TProperty> AddDecorator(IDecorator<T, TProperty> decorator)
    {
        decorator.Specification = _propertyRule.Specifications.Last();
        _propertyRule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyRule<T, TProperty> Build() => _propertyRule;
}