namespace InfiniteValidation.Results;

public class ValidationResult
{
    public ValidationOptions Options { get; }
    
    public List<ValidationFailure> Errors { get; } = new();

    public bool IsValid => Options.OnlyInvalidOnErrorSeverity
        ? Errors.All(x => x.Severity != Severity.Error)
        : Errors.Count == 0;

    public ValidationResult(ValidationOptions options)
    {
        Options = options;
    }
}