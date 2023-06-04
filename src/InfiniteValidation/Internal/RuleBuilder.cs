namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleBuilderInitial<T, TProperty>, IRuleBuilderDecorator<T, TProperty>
{
    private readonly IPropertyRule<T, TProperty> _rule;

    public RuleBuilder(IPropertyRule<T, TProperty> rule)
    {
        _rule = rule;
    }

    public IRuleBuilderInitial<T, TProperty> OverridePropertyName(string name)
    {
        _rule.PropertyName = name;
        return this;
    }

    public IRuleBuilderInitial<T, TProperty> Cascade(CascadeMode mode)
    {
        _rule.CascadeMode = mode;
        return this;
    }
    
    public IRuleBuilderInitial<T, TProperty> Include(IValidator<TProperty> validator)
    {
        validator.Guard(nameof(validator));
        var rules = validator.GetRulesets().SelectMany(ruleset => ruleset.GetRules()).ToList();
        _rule.Rules.AddRange(rules);
        return this;
    }

    public IRuleBuilderDecorator<T, TProperty> Specify(ISpecification<T, TProperty> specification)
    {
        specification.Guard(nameof(specification));
        _rule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderDecorator<T, TProperty> Decorate(IDecorator<T, TProperty> decorator)
    {
        decorator.Guard(nameof(decorator));
        _rule.Specifications.ReplaceLast(decorator);
        return this;
    }

    public ISpecification<T, TProperty> GetSpecificationToDecorate() =>  _rule.Specifications.Last();

    internal IPropertyRule<T, TProperty> Build() => _rule;
}