using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class Ruleset<T> : IRuleset<T>
{
    private Func<ValidationContext<T>, bool> _condition;

    private readonly IEnumerable<IValidatorRule<T>> _rules;

    private readonly CascadeMode _cascadeMode;

    public Ruleset(Func<ValidationContext<T>, bool> condition, IEnumerable<IValidatorRule<T>> rules, CascadeMode cascadeMode)
    {
        _condition = condition;
        _rules = rules;
        _cascadeMode = cascadeMode;
    }
    
    public IEnumerable<ValidationFailure> Validate(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();

        foreach (var rule in _rules)
        {
            failures.AddRange(rule.Validate(context));
            if(_cascadeMode == CascadeMode.Stop) break;
        }
        
        return failures;
    }
    
    public Func<ValidationContext<T>, bool> GetCondition() => _condition;

    public IEnumerable<IValidatorRule<T>> GetRules() => _rules;
}