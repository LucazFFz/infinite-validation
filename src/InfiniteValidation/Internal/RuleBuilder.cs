namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleSettingsBuilder<T, TProperty>
{
    private readonly IRule<T, TProperty> _rule;

    public RuleBuilder(IRule<T, TProperty> rule)
    {
        _rule = rule;
    }

    public IRuleSettingsBuilder<T, TProperty> CascadeMode(CascadeMode mode)
    {
        _rule.CascadeMode = mode;
        return this;
    }
    
    public IRuleBuilder<T, TProperty> Must(ISpecification<T, TProperty> specification)
    {
        _rule.Specifications.Add(specification);
        return this;
    }

    internal IRule<T, TProperty> Build() => _rule;
}