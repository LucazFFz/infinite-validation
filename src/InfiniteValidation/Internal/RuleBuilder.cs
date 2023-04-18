namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleBuilder<T, TProperty>
{
    private readonly IRule<T, TProperty> _rule;

    public RuleBuilder(IRule<T, TProperty> rule)
    {
        _rule = rule;
    }
    
    public IRuleBuilder<T, TProperty> Must(ISpecification<T, TProperty> specification)
    {
        _rule.AddSpecification(specification);
        return this;
    }

    internal IRule<T, TProperty> Build() => _rule;
}