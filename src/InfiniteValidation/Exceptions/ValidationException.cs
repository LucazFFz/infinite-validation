using InfiniteValidation.Results;

namespace InfiniteValidation.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<SpecificationFailure> Errors { get; private set; } = Enumerable.Empty<SpecificationFailure>();

    public ValidationException() {}

    public ValidationException(string message) : base(message)  {}

    public ValidationException(string message, IEnumerable<SpecificationFailure> errors) : base(message) 
        =>  Errors = errors;

    public ValidationException(IEnumerable<SpecificationFailure> errors) : base(BuildErrorMessage(errors))
        => Errors = errors;

    private static string BuildErrorMessage(IEnumerable<SpecificationFailure> errors)
    {
        var arr = errors.Select(x =>
            $"{Environment.NewLine} -- {x.SpecificationName}: {x.FailureMessage}: {x.Severity.ToString()}");
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}