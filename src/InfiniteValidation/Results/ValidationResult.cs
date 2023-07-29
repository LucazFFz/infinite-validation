namespace InfiniteValidation.Results;

/// <summary>
/// The result of preforming a validation.
/// </summary>
[Serializable]
public sealed class ValidationResult
{
    /// <summary>
    /// The settings used for the validation.
    /// </summary>
    public ValidationSettings Settings { get; }
    
    /// <summary>
    /// A collection of validation failures.
    /// </summary>
    public List<ValidationFailure> Failures { get; } = new();

    /// <summary>
    /// Determines weather or not the validation is valid.
    /// </summary>
    public bool UnconditionalIsValid => Failures.Any();

    /// <summary>
    /// Determines weather or not the validation is valid.
    /// </summary>
    /// <remarks>
    /// If the OnlyInvalidOnErrorSeverity setting is set to true, this will return false
    /// only if there are failures with the Error severity present.
    /// </remarks>
    public bool IsValid => Settings.OnlyInvalidOnErrorSeverity
        ? Failures.All(x => x.Severity != Severity.Error)
        : Failures.Any();
    
    public ValidationResult(ValidationSettings settings)
    {
        Settings = settings;
    }

    /// <summary>
    /// Generates a string representation of all failure messages seperated by a new line.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => ToString(Environment.NewLine);
    
    /// <summary>
    /// Generates a string representation of all failure messages seperated by the specified string.
    /// </summary>
    /// <param name="separator">The string by which the failure messages are seperated.</param>
    /// <returns></returns>
    public string ToString(string separator)
        => string.Join(separator, Failures.Select(x => x.FailureMessage));
    
    /// <summary>
    /// Converts the ValidationResult's failure collection into a dictionary representation. 
    /// </summary>
    /// <returns>
    /// A dictionary keyed by property name where each value is an
    /// array of failure messages associated with that property.
    /// </returns>
    public Dictionary<string, string[]> ToDictionary()
    {
        return Failures
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key, 
                g => g.Select(x => x.FailureMessage).ToArray()
            );
    }
}