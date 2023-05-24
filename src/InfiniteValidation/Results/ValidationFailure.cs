namespace InfiniteValidation.Results;

public class ValidationFailure
{
    public string SpecificationName { get; } 
    
    public string FailureMessage { get; } 

    public object? AttemptedValue { get; }
    
    public Severity Severity { get; } 

    public ValidationFailure(
        string specificationName, 
        string failureMessage, 
        object? attemptedValue, 
        Severity severity = Severity.Error)
    {
        SpecificationName = specificationName;
        FailureMessage = failureMessage;
        AttemptedValue = attemptedValue;
        Severity = severity;
    }
}

