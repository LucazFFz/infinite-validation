using InfiniteValidation.Results;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class ValidationResultTests
{
    [Fact]
    public void Valid_when_there_are_no_errors()
    {
        var result = new ValidationResult(new ValidationSettings());
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Invalid_when_there_are_errors()
    {
        var result = new ValidationResult(new ValidationSettings())
        {
            Failures = {new ValidationFailure(null, null, null, null), new ValidationFailure(null, null, null, null)}
        };
        
        Assert.False(result.IsValid);
    }

     [Fact]
    public void Add_errors_correctly()
    {
        var result = new ValidationResult(new ValidationSettings())
        {
            Failures = {new ValidationFailure(null, null, null, null), new ValidationFailure(null, null, null, null)}
        };
        
        Assert.Equal(2, result.Failures.Count);
    }

    [Fact]
    public void Valid_when_there_are_no_failures_with_error_severity_if_OnlyInvalidOnErrorSeverity_is_true()
    {
        var result = new ValidationResult(new ValidationSettings {OnlyInvalidOnErrorSeverity = true})
        {
            Failures = 
            {
                new ValidationFailure(null, null, null, null, Severity.Info) , 
                new ValidationFailure(null, null, null, null, Severity.Warning),
            }
        };
        
        Assert.True(result.IsValid);   
    }
    
    [Fact]
    public void InValid_when_there_are_failures_with_error_severity_even_if_OnlyInvalidOnErrorSeverity_is_true()
    {
        var result = new ValidationResult(new ValidationSettings {OnlyInvalidOnErrorSeverity = true})
        {
            Failures = 
            {
                new ValidationFailure(null, null, null, null, Severity.Info) , 
                new ValidationFailure(null, null, null, null, Severity.Warning),
                new ValidationFailure(null, null, null, null)
            }
        };
        
        Assert.False(result.IsValid);   
    }
}