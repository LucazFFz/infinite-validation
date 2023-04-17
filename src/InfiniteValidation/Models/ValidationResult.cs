namespace InfiniteValidation.Models;

public class ValidationResult
{
    public ValidationOptions Options { get; }
    
    public List<ValidationFailure> Errors { get; } = new();

    public bool IsValid => Options.OnlyInvalidOnErrorSeverity
        ? Errors.Any(x => x.Severity == Severity.Error)
        : Errors.Count != 0;

    public ValidationResult(ValidationOptions options)
    {
        Options = options;
    }
}