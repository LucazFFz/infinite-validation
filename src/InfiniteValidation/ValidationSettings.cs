namespace InfiniteValidation;

public class ValidationSettings
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;

    public List<string> RulesetsToValidate { get; } = new();

    internal bool ShouldValidateRuleset<T>(IRuleset<T> ruleset)
        => RulesetsToValidate.Contains(ruleset.GetKey()) ||
           ruleset.GetKey() == Validator<dynamic>.DefaultRulesetKey;
}