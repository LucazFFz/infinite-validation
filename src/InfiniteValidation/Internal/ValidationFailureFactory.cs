using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal static class ValidationFailureFactory
{
    public static ValidationFailure Create<T, TProperty>(ISpecification<T, TProperty> specification, TProperty value)
    {
        return new ValidationFailure
        {
            FailureMessage = specification.MessageBuilder.BuildMessage(specification.GetMessageFormat()),
            SpecificationName = specification.GetSpecificationName(),
            Severity = specification.GetSeverity(),
            AttemptedValue = value
        };
    }
}