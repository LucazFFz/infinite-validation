namespace InfiniteValidation;

public interface IRuleBuilderSettings<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleBuilderSettings<T, TProperty> AddDecorator(IDecorator<T, TProperty> decorator);
}