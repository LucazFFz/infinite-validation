namespace InfiniteValidation;

public interface ICollectionRuleBuilderInitial<T, TElement> : IRuleBuilder<T, TElement>
{
    public ICollectionRuleBuilderInitial<T, TElement> PropertyName(string propertyName);
    
    public ICollectionRuleBuilderInitial<T, TElement> CascadeMode(CascadeMode mode);

    public ICollectionRuleBuilderInitial<T, TElement> Where(Func<TElement, bool> condition);

    public ICollectionRuleBuilderInitial<T, TElement> Include(IValidator<TElement> validator);
}