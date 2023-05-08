namespace InfiniteValidation.Internal;

internal class RuleSetBuilder<T> : IRuleSetBuilder<T>
{
    private IRuleSet<T> _ruleSet;

    public RuleSetBuilder(IRuleSet<T> ruleSet)
    {
        _ruleSet = ruleSet;
    }

    public IRuleSetBuilder<T> Decorate(IRuleSetDecorator<T> decorator)
    {
        decorator.RuleSet = _ruleSet;
        _ruleSet = decorator;

        return this;
    }

    internal IRuleSet<T> Build() => _ruleSet;
}