using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal static class ValidationFailureFactory
{
    public static ValidationFailure Create<T, TProperty>(ISpecification<T, TProperty> specification, TProperty property, string propertyName)
    {
        specification.MessageBuilder.AppendPropertyName(propertyName).AppendAttemptedValue(property);
        
        return new ValidationFailure(
            specification.GetName(),
            propertyName,
            specification.MessageBuilder.Build(specification.GetMessageFormat()),
            property, specification.GetSeverity());
    }
}