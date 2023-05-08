using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal class RuleSet<T> : IRuleSet<T>
{
    private readonly string _name;

    private readonly IEnumerable<IValidatorRule<T>> _rules;

    public RuleSet(string name, IEnumerable<IValidatorRule<T>> rules)
    {
        _rules = rules;
        _name = name;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();

        foreach (var rule in _rules)
        {
            failures.AddRange(rule.IsValid(context));
            //if (CascadeMode == CascadeMode.Stop) break;
        }
        
        return failures;
    }
    
    public string GetName() => _name;

    public IEnumerable<IValidatorRule<T>> GetRules() => _rules;
}