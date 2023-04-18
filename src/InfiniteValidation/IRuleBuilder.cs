namespace InfiniteValidation;

public interface IRuleBuilder<T, out TProperty>
{
    public IRuleBuilder<T, TProperty> Must(ISpecification<T, TProperty> specification);
}