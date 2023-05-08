namespace InfiniteValidation;

public class ValidationSettings
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;
    
    public List<string> RuleSetsToValidate { get; } = new();
}