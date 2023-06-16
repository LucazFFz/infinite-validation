using System.Text.RegularExpressions;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Extensions;

public static class StringExtensions
{
    public static bool Matches(this string str, string regularExpression)
        => Matches(str, new Regex(regularExpression));
    
    public static bool Matches(this string str, Regex regularExpression)
        => new RegexSpecification<string>(regularExpression)
            .IsSatisfiedBy(new ValidationContext<string>(str), str);
}