namespace InfiniteValidation.Internal;

internal class CollectionRuleBuilder<T, TElement> : ICollectionRuleBuilderInitial<T, TElement>, IRuleBuilderSettings<T, TElement>
{
    private readonly IPropertyCollectionRule<T, TElement> _propertyCollectionRule;

    public CollectionRuleBuilder(IPropertyCollectionRule<T, TElement> propertyCollectionRule)
    {
        _propertyCollectionRule = propertyCollectionRule;
    }

    public ICollectionRuleBuilderInitial<T, TElement> CascadeMode(CascadeMode mode)
    {
        _propertyCollectionRule.CascadeMode = mode;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Where(Func<TElement, bool> condition)
    {
        _propertyCollectionRule.ShouldValidateChildCondition = condition;
        return this;
    }

    public ICollectionRuleBuilderInitial<T, TElement> Include(IValidator<TElement> validator)
    {
        _propertyCollectionRule.ChildValidator = validator;
        return this;
    }

    public IRuleBuilderSettings<T, TElement> AddSpecification(ISpecification<T, TElement> specification)
    {
        _propertyCollectionRule.Specifications.Add(specification);
        return this;
    }
    
    public IRuleBuilderSettings<T, TElement> Decorate(IDecorator<T, TElement> decorator)
    {
        decorator.Specification = _propertyCollectionRule.Specifications.Last();
        _propertyCollectionRule.Specifications.ReplaceLast(decorator);
        return this;
    }

    internal IPropertyCollectionRule<T, TElement> Build() => _propertyCollectionRule;
}