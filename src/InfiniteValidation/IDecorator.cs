namespace InfiniteValidation;

public interface IDecorator<T, TProperty> : ISpecification<T, TProperty>
{
    public ISpecification<T, TProperty> Specification { get; set; }
}