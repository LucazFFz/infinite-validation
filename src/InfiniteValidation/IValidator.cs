using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidator<T>
{
    public ValidationResult Validate(T instance, ValidationSettings settings);
    
    public List<IRuleset<T>> GetRulesets();
}