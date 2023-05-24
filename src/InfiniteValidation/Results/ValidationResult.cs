namespace InfiniteValidation.Results;

public class ValidationResult
{
    public ValidationSettings Settings { get; }
    
    public List<ValidationFailure> Failures { get; } = new();

    public bool UnconditionalIsValid => Failures.Any();

    public bool IsValid => Settings.OnlyInvalidOnErrorSeverity
        ? Failures.All(x => x.Severity != Severity.Error)
        : Failures.Any();

    public ValidationResult(ValidationSettings settings)
    {
        Settings = settings;
    }
}