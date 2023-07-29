using InfiniteValidation.Results;

namespace InfiniteValidation.Exceptions;

public sealed class ValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; private set; } = Enumerable.Empty<ValidationFailure>();

    public ValidationException() {}

    public ValidationException(string message) : base(message)  {}

    public ValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message) 
        =>  Errors = errors;

    public ValidationException(IEnumerable<ValidationFailure> errors) : base(BuildErrorMessage(errors))
        => Errors = errors;

    private static string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
    {
        var arr = errors.Select(x =>
            $"{Environment.NewLine} -- {x.SpecificationName}: {x.FailureMessage}: {x.Severity.ToString()}");
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}