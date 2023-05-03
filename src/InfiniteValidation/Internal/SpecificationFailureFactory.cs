using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal static class SpecificationFailureFactory
{
    public static SpecificationFailure Create<T, TProperty>(ISpecification<T, TProperty> specification, TProperty value)
    {
        return new SpecificationFailure(
            specification.GetSpecificationName(), 
            specification.MessageBuilder.BuildMessage(specification.GetMessageFormat()), 
            value)
        {
            Severity = specification.GetSeverity()
        };
    }
}