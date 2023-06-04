using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IRuleset<T>
{
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context);

    public IEnumerable<IValidatorRule<T>> GetRules();

    public Func<ValidationContext<T>, bool> GetCondition();
}