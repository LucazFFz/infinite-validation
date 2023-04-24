using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class ObjectExtensions
{
    public static bool Must<T>(this T obj, Func<T, bool> predicate)
        => new PredicateSpecification<T, T>(predicate).IsSatisfiedBy(null!, obj);
    
    public static bool IsNull<T>(this T obj)
        => new NullSpecification<T, T>().IsSatisfiedBy(null!, obj);

    public static bool NotNull<T>(this T obj)
        => !new NullSpecification<T, T>().IsSatisfiedBy(null!, obj);
}