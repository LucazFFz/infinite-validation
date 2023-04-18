using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IRuleSettingsBuilder<T, out TProperty> : IRuleBuilder<T, TProperty>
{
    public IRuleSettingsBuilder<T, TProperty> CascadeMode(CascadeMode mode);
}