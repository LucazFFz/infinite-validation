namespace InfiniteValidation.Results;

public class SpecificationFailure
{
    public string SpecificationName { get; internal set; } 
    
    public string FailureMessage { get; internal set; } 

    public object? AttemptedValue { get; internal set; }
    
    public Severity Severity { get; internal set; } 

    public SpecificationFailure(string specificationName, string failureMessage, object? attemptedValue, Severity severity = Severity.Error)
    {
        SpecificationName = specificationName;
        FailureMessage = failureMessage;
        AttemptedValue = attemptedValue;
        Severity = severity;
    }
}

