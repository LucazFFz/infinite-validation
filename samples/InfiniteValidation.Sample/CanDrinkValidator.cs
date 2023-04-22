using System.Data;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Sample;

public class CanDrinkValidator : Validator<Person>
{
    public CanDrinkValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;
        ClassLevelCascadeMode = CascadeMode.Continue;

        AddRule(x => x.Age).CascadeMode(CascadeMode.Stop).NotNull().WithMessage(
            new PredicateSpecification<Person, int>(x => x >= 18), "You are not old enough to drink");
    }
}