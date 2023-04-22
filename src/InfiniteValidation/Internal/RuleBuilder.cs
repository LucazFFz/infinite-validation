namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleSettingsBuilder<T, TProperty>
{
    private readonly IPropertyRule<T, TProperty> _propertyRule;

    public RuleBuilder(IPropertyRule<T, TProperty> propertyRule)
    {
        _propertyRule = propertyRule;
    }

    public IRuleSettingsBuilder<T, TProperty> CascadeMode(CascadeMode mode)
    {
        _propertyRule.CascadeMode = mode;
        return this;
    }
    
    public IRuleBuilder<T, TProperty> Must(ISpecification<T, TProperty> specification)
    {
        _propertyRule.Specifications.Add(specification);
        return this;
    }

    internal IPropertyRule<T, TProperty> Build() => _propertyRule;
}