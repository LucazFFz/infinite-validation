namespace InfiniteValidation.Results;

public class ValidationFailure
{
    public string SpecificationName { get; internal set; } = string.Empty;
    
    public string FailureMessage { get; internal set; } = string.Empty;

    public object? AttemptedValue { get; internal set; }
    
    public Severity Severity { get; internal set; } = Severity.Error;
}

