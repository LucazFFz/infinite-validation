namespace InfiniteValidation;

public interface IRuleBuilderInitial<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleBuilderInitial<T, TProperty> CascadeMode(CascadeMode mode);
}