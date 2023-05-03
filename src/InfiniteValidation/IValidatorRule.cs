using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidatorRule<T>
{ 
    public IEnumerable<SpecificationFailure> IsValid(ValidationContext<T> context);
}