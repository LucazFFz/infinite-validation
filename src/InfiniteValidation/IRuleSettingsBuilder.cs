namespace InfiniteValidation;

public interface IRuleSettingsBuilder<T, TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleSettingsBuilder<T, TProperty> CascadeMode(CascadeMode mode);
}