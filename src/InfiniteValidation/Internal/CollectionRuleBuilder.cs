namespace InfiniteValidation.Internal;

internal class CollectionRuleBuilder<T, TElement> : ICollectionRuleBuilderInitial<T, TElement>, IRuleBuilderSettings<T, TElement>
{
    private readonly IPropertyCollectionRule<T, TElement> _collectionRule;

    public CollectionRuleBuilder(IPropertyCollectionRule<T, TElement> collectionRule)
    {
        _collectionRule = collectionRule;
    }
    
    public ICollectionRuleBuilderInitial<T, TElement> PropertyName(string propertyName)
    {
        _collectionRule.PropertyName = propertyName;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> CascadeMode(CascadeMode mode)
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
        _collectionRule.ChildValidator = validator;
        return this;
    }

    public IRuleBuilderSettings<T, TElement> AddSpecification(ISpecification<T, TElement> specification)
    {
        _collectionRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderSettings<T, TElement> Decorate(IDecorator<T, TElement> decorator)
    {
        decorator.Specification = _collectionRule.Specifications.Last();
        _collectionRule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyCollectionRule<T, TElement> Build() => _collectionRule;
}