using InfiniteValidation.Internal;
using InfiniteValidation.Results;

namespace InfiniteValidation.RuleSetDecorators;

public class RuleSetWhenDecorator<T> : IRuleSetDecorator<T>
{
    public IRuleSet<T> RuleSet { get; set; }

    private readonly Func<T, bool> _condition;

    public RuleSetWhenDecorator(Func<T, bool> condition)
    {
        _condition = condition;
    }

    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        return _condition.Invoke(context.InstanceToValidate) ? RuleSet.IsValid(context) : new List<ValidationFailure>();
    }

    public IEnumerable<IValidatorRule<T>> GetRules() => RuleSet.GetRules();

    public string GetName() => RuleSet.GetName();
}