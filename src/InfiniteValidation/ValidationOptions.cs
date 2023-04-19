namespace InfiniteValidation;

public class ValidationOptions
{
    public bool ThrowExceptionOnInvalid { get; set; } = false;

    public bool OnlyInvalidOnErrorSeverity { get; set; } = true;
}