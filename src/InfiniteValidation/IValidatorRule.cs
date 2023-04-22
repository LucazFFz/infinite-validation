using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidatorRule<T>
{ 
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);
}