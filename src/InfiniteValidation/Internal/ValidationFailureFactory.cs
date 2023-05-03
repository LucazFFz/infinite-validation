using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal static class ValidationFailureFactory
{
    public static ValidationFailure Create<T, TProperty>(ISpecification<T, TProperty> specification, TProperty value)
    {
        return new ValidationFailure(
            specification.GetSpecificationName(), 
            specification.MessageBuilder.BuildMessage(specification.GetMessageFormat()), 
            value)
        {
            Severity = specification.GetSeverity()
        };
    }
}