using System.Data;
using InfiniteValidation;
using InfiniteValidation.Decorators;
using InfiniteValidation.Extensions;
using InfiniteValidation.Models;
using InfiniteValidation.Specifications;

namespace ValidationLibrary.Console;

public class CanDrinkValidator : Validator<Person>
{
    public CanDrinkValidator()
    {
        RuleLevelDefaultCascadeMode = CascadeMode.Continue;
        ClassLevelDefaultCascadeMode = CascadeMode.Continue;

        AddRule(x => x.Age).CascadeMode(CascadeMode.Stop).NotNull().WithMessage(
            new PredicateSpecification<Person, dynamic>(x => x >= 18), "You are not old enough to drink");
    }
}