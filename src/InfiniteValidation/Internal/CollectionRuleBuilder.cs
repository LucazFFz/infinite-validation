namespace InfiniteValidation.Internal;

internal class CollectionRuleBuilder<T, TElement> : ICollectionRuleBuilderInitial<T, TElement>, IRuleBuilderDecorator<T, TElement>
{
    private readonly IPropertyCollectionRule<T, TElement> _collectionRule;

    public CollectionRuleBuilder(IPropertyCollectionRule<T, TElement> collectionRule)
    {
        _collectionRule = collectionRule;
    }
    
    public ICollectionRuleBuilderInitial<T, TElement> OverridePropertyName(string name)
    {
        _collectionRule.PropertyName = name;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Cascade(CascadeMode mode)
    {
        _collectionRule.CascadeMode = mode;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Where(Func<TElement, bool> condition)
    {
        _collectionRule.ShouldValidateChildCondition = condition;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Include(IValidator<TElement> validator)
    {
        _collectionRule.Rulesets.AddRange(validator.GetRulesets());
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Include(Action<IInlineValidator<TElement>> action)
    {
        var inlineValidator = new InlineValidator<TElement>();
        action.Invoke(inlineValidator);
        _collectionRule.Rulesets.AddRange(inlineValidator.GetRulesets());
        return this;
    }
    
    public IRuleBuilderDecorator<T, TElement> Specify(ISpecification<T, TElement> specification)
    {
        _collectionRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderDecorator<T, TElement> Decorate(IDecorator<T, TElement> decorator)
    {
        decorator.Specification = _collectionRule.Specifications.Last();
        _collectionRule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyCollectionRule<T, TElement> Build() => _collectionRule;
}