namespace InfiniteValidation;

public interface IRuleBuilder<T, TProperty>
{
    public IRuleBuilder<T, TProperty> AddSpecification(ISpecification<T, TProperty> specification);

    public IRuleBuilder<T, TProperty> AddDecorator(ISpecification<T, TProperty> decorator);

    public ISpecification<T, TProperty> GetActiveSpecification();
}