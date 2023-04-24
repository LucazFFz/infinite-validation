namespace InfiniteValidation.Results;

public class ValidationResult
{
    public ValidationSettings Settings { get; }
    
    public List<ValidationFailure> Errors { get; } = new();

    public bool IsValid => Settings.OnlyInvalidOnErrorSeverity
        ? Errors.All(x => x.Severity != Severity.Error)
        : Errors.Count == 0;

    public ValidationResult(ValidationSettings settings)
    {
        Settings = settings;
    }
}