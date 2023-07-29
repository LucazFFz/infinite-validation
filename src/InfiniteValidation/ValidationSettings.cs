namespace InfiniteValidation;

public sealed class ValidationSettings
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;

    public List<string> RulesetsToValidate { get; } = new();

    public bool ShouldValidateRuleset<T>(IRuleset<T> ruleset) =>
        RulesetsToValidate.Contains(ruleset.GetKey()) ||
        ruleset.GetKey() == Validator<dynamic>.DefaultRulesetKey;
}