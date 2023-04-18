using InfiniteValidation.Extensions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Internal;

internal static class ValidationFailureFactory
{
    public static ValidationFailure Create<T, TProperty>(ISpecification<T, TProperty> specification, TProperty value)
    {
        value.Guard(specification.GetSpecificationName());
        
        return new ValidationFailure
        {
            FailureMessage = specification.GetErrorMessage(),
            SpecificationName = specification.GetSpecificationName(),
            Severity = specification.GetSeverity(),
            AttemptedValue = value
        };
    }
}