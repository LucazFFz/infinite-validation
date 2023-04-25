using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class ObjectExtensions
{
    public static bool Must<T>(this T obj, Func<T, bool> predicate)
        => new PredicateSpecification<T, T>(predicate)
            .IsSatisfiedBy(new ValidationContext<T>(obj), obj);
    
    public static bool IsNull<T>(this T obj)
        => new NullSpecification<T, T>()
            .IsSatisfiedBy(new ValidationContext<T>(obj), obj);

    public static bool IsNotNull<T>(this T obj)
        => !new NullSpecification<T, T>()
            .IsSatisfiedBy(new ValidationContext<T>(obj), obj);
}