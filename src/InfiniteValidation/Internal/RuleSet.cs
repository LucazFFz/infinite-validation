using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal sealed class Ruleset<T> : IRuleset<T>
{
    private readonly string _key;

    private readonly IEnumerable<IValidatorRule<T>> _rules;

    private readonly CascadeMode _cascadeMode;

    public Ruleset(string key, IEnumerable<IValidatorRule<T>> rules, CascadeMode cascadeMode)
    {
        _key = key;
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
    
    public string GetKey() => _key;

    public IEnumerable<IValidatorRule<T>> GetRules() => _rules;
}