using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IRuleSet<T>
{
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context);

    public IEnumerable<IValidatorRule<T>> GetRules();

    public string GetName();
}