using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidator<T>
{
    public ValidationResult Validate(T instance);

    public ValidationResult Validate(T instance, ValidationSettings settings);

    public ValidationResult Validate(T instance, Action<ValidationSettings> settings);

    public List<IValidatorRule<T>> GetRules();
}