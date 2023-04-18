namespace InfiniteValidation.Models;

public class ValidationOptions
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;
    
    public CascadeMode RuleLevelDefaultCascadeMode { get; set; } = CascadeMode.Continue;
}

public enum CascadeMode
{
    Continue,
    Stop
}