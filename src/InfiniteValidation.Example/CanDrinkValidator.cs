using InfiniteValidation;
using InfiniteValidation.Decorators;
using InfiniteValidation.Extensions;
using InfiniteValidation.Models;
using InfiniteValidation.Specifications;

namespace ValidationLibrary.Console;

public class CanDrinkValidator : AbstractValidator<Person>
{
    public CanDrinkValidator()
    {
        AddRule(x => x.Age).Must(x => x >= 18);
    }
}