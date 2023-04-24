namespace InfiniteValidation;

public interface IRuleBuilder<T, TProperty>
{
    public IRuleBuilderSettings<T, TProperty> AddSpecification(ISpecification<T, TProperty> specification);
}