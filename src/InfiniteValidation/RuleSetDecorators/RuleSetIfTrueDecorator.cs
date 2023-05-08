using InfiniteValidation.Internal;
using InfiniteValidation.Results;

namespace InfiniteValidation.RuleSetDecorators;

public class RuleSetIfTrueDecorator<T> : IRuleSetDecorator<T>
{
    public IRuleSet<T> RuleSet { get; set; }

    private IRuleSet<T> _ruleSet;

    public RuleSetIfTrueDecorator(IRuleSet<T> ruleSet)
    {
        _ruleSet = ruleSet;
    }


    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        if (!RuleSet.IsValid(context).Any()) return _ruleSet.IsValid(context);
        return RuleSet.IsValid(context);
    }

    public IEnumerable<IValidatorRule<T>> GetRules() => RuleSet.GetRules().Concat(_ruleSet.GetRules());
    
    public string GetName() => RuleSet.GetName();
}