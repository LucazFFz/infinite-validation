namespace InfiniteValidation.Internal;

internal sealed class CollectionRuleBuilder<T, TElement> : ICollectionRuleBuilderInitial<T, TElement>, IRuleBuilderDecorator<T, TElement>
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

    public ICollectionRuleBuilderInitial<T, TElement> Include(IValidator<TElement> childValidator)
    {
        childValidator.Guard(nameof(childValidator));
        var rules = childValidator.GetRulesets().SelectMany(ruleset => ruleset.GetRules()).ToList();
        _collectionRule.Rules.AddRange(rules);
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Include(Action<IInlineValidator<TElement>> action)
    {
        action.Guard(nameof(action));
        var inlineValidator = new InlineValidator<TElement>();
        action.Invoke(inlineValidator);
        Include(inlineValidator);
        return this;
    }
    
    public IRuleBuilderDecorator<T, TElement> Specify(ISpecification<T, TElement> specification)
    {
        _collectionRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderDecorator<T, TElement> Decorate(IDecorator<T, TElement> decorator)
    {
        _collectionRule.Specifications.ReplaceLast(decorator);
        return this;
    }
    
    public ISpecification<T, TElement> GetSpecificationToDecorate() =>  _collectionRule.Specifications.Last();

    internal IPropertyCollectionRule<T, TElement> Build() => _collectionRule;
}