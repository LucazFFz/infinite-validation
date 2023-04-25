namespace InfiniteValidation;

public interface IRuleBuilderSettings<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleBuilderSettings<T, TProperty> Decorate(IDecorator<T, TProperty> decorator);
}