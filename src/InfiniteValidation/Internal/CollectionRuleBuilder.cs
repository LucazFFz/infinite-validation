namespace InfiniteValidation.Internal;

internal class CollectionRuleBuilder<T, TProperty, TElement> : ICollectionRuleBuilderInitial<T, TElement>, IRuleBuilderSettings<T, TElement>
    where TProperty : IEnumerable<TElement>
{
    private readonly IPropertyCollectionRule<T, TProperty, TElement> _propertyCollectionRule;

    public CollectionRuleBuilder(IPropertyCollectionRule<T, TProperty, TElement> propertyCollectionRule)
    {
        _propertyCollectionRule = propertyCollectionRule;
    }

    public ICollectionRuleBuilderInitial<T, TElement> CascadeMode(CascadeMode mode)
    {
        _propertyCollectionRule.CascadeMode = mode;
        return this;
    }
    
    public IRuleBuilderSettings<T, TElement> AddSpecification(ISpecification<T, TElement> specification)
    {
        _propertyCollectionRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderSettings<T, TElement> AddDecorator(IDecorator<T, TElement> decorator)
    {
        decorator.Specification = _propertyCollectionRule.Specifications.Last();
        _propertyCollectionRule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyCollectionRule<T, TProperty, TElement> Build() => _propertyCollectionRule;
}