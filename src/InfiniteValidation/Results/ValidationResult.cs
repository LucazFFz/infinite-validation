namespace InfiniteValidation.Results;

public class ValidationResult
{
    public ValidationSettings Settings { get; }
    
    public List<SpecificationFailure> Failures { get; } = new();

    public bool IsValid => Settings.OnlyInvalidOnErrorSeverity
        ? Failures.All(x => x.Severity != Severity.Error)
        : Failures.Count == 0;

    public ValidationResult(ValidationSettings settings)
    {
        Settings = settings;
    }
}