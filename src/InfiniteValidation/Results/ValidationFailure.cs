namespace InfiniteValidation.Results;

public sealed class ValidationFailure
{
    public string SpecificationName { get; } 
    
    public string PropertyName { get; }
    
    public string FailureMessage { get; } 

    public object? AttemptedValue { get; }
    
    public Severity Severity { get; } 

    public ValidationFailure(
        string specificationName, 
        string propertyName,
        string failureMessage, 
        object? attemptedValue, 
        Severity severity = Severity.Error)
    {
        SpecificationName = specificationName;
        PropertyName = propertyName;
        FailureMessage = failureMessage;
        AttemptedValue = attemptedValue;
        Severity = severity;
    }
}

