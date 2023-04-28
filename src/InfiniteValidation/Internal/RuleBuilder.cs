namespace InfiniteValidation.Internal;

internal class RuleBuilder<T, TProperty> : IRuleBuilderInitial<T, TProperty>, IRuleBuilderSettings<T, TProperty>
{
    private readonly IPropertyRule<T, TProperty> _rule;

    public RuleBuilder(IPropertyRule<T, TProperty> rule)
    {
        _rule = rule;
    }

    public IRuleBuilderInitial<T, TProperty> PropertyName(string propertyName)
    {
        _rule.PropertyName = propertyName;
        return this;
    }

    public IRuleBuilderInitial<T, TProperty> CascadeMode(CascadeMode mode)
    {
        _rule.CascadeMode = mode;
        return this;
    }

    public IRuleBuilderInitial<T, TProperty> Include(IValidator<TProperty> validator)
    {
        _rule.ChildValidator = validator;
        return this;
    }
    
    public IRuleBuilderSettings<T, TProperty> AddSpecification(ISpecification<T, TProperty> specification)
    {
        _rule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderSettings<T, TProperty> Decorate(IDecorator<T, TProperty> decorator)
    {
        decorator.Specification = _rule.Specifications.Last();
        _rule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyRule<T, TProperty> Build() => _rule;
}