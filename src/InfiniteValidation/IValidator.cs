using InfiniteValidation.Models;

namespace InfiniteValidation;

public interface IValidator<in T>
{ 
    public ValidationResult Validate(T instance);
}