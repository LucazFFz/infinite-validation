using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IValidator<T>
{
    public ValidationResult Validate(T instance);

    public List<IRule<T, dynamic>> GetRules();
}