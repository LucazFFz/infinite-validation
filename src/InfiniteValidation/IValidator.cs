using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidator<T>
{
    public ValidationResult Validate(T instance);

    public List<IValidatorRule<T>> GetRules();
}