namespace InfiniteValidation;

public interface IRuleBuilder<T, TProperty>
{
    public IRuleBuilderDecorator<T, TProperty> Specify(ISpecification<T, TProperty> specification);
}

public interface IRuleBuilderInitial<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleBuilderInitial<T, TProperty> OverridePropertyName(string name);

    public IRuleBuilderInitial<T, TProperty> Cascade(CascadeMode mode);
    
    public IRuleBuilderInitial<T, TProperty> Include(IValidator<TProperty> validator);
}

public interface IRuleBuilderDecorator<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleBuilderDecorator<T, TProperty> Decorate(IDecorator<T, TProperty> decorator);
    
    public ISpecification<T, TProperty> GetSpecificationToDecorate();
}

public interface ICollectionRuleBuilderInitial<T, TElement> : IRuleBuilder<T, TElement>
{
    public ICollectionRuleBuilderInitial<T, TElement> OverridePropertyName(string name);
    
    public ICollectionRuleBuilderInitial<T, TElement> Cascade(CascadeMode mode);
    
    public ICollectionRuleBuilderInitial<T, TElement> Include(IValidator<TElement> validator);
    
    public ICollectionRuleBuilderInitial<T, TElement> Include(Action<IInlineValidator<TElement>> action);

    public ICollectionRuleBuilderInitial<T, TElement> Where(Func<TElement, bool> condition);
}





