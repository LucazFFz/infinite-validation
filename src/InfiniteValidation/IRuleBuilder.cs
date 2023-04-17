namespace InfiniteValidation;

public interface IRuleBuilder<T, TProperty>
{
    public IRuleBuilder<T, TProperty> Must(ISpecification<T, TProperty> specification);

    public IRule<T, TProperty> Build();
}