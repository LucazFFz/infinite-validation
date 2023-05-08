using InfiniteValidation.Results;

namespace InfiniteValidation;

public interface IValidator<T>
{
    public IRuleSetBuilder<T> RuleSet(string name, IEnumerable<IValidatorRule<T>> rules);
    
    public ValidationResult Validate(T instance);

    public ValidationResult Validate(T instance, ValidationSettings settings);

    public ValidationResult Validate(T instance, Action<ValidationSettings> settings);

    public List<IRuleSet<T>> GetRuleSets();
}