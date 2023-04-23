using System.Text.RegularExpressions;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class StringExtensions
{
    public static bool Match(this string str, string regularExpression)
        => new RegularExpressionSpecification<string>(regularExpression).IsSatisfiedBy(null!, str);
    
    public static bool Match(this string str, Regex regularExpression)
        => new RegularExpressionSpecification<string>(regularExpression).IsSatisfiedBy(null!, str);
}