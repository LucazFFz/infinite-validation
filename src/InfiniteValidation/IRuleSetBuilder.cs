namespace InfiniteValidation;

public interface IRuleSetBuilder<T>
{
    public IRuleSetBuilder<T> Decorate(IRuleSetDecorator<T> decorator);
}