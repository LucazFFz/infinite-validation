namespace InfiniteValidation.Models;

public class ValidationOptions
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;
}

public enum CascadeMode
{
    Continue,
    Stop
}