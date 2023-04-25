using System.Text.RegularExpressions;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class StringExtensions
{
    public static bool Matches(this string str, string regularExpression)
        => new RegularExpressionSpecification<string>(regularExpression)
            .IsSatisfiedBy(new ValidationContext<string>(str), str);
    
    public static bool Matches(this string str, Regex regularExpression)
        => new RegularExpressionSpecification<string>(regularExpression)
            .IsSatisfiedBy(new ValidationContext<string>(str), str);
}