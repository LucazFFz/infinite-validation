using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class ObjectExtensions
{
    public static bool Fulfills<T>(this T obj, Func<T, bool> predicate)
        => new PredicateSpecification<T, T>(predicate)
            .IsSatisfiedBy(new ValidationContext<T>(obj), obj);
}