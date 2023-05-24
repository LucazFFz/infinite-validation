using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidatorRule<T>
{ 
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context);
}