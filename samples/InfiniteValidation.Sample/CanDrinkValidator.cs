using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Sample;

public class CanDrinkValidator : Validator<Person>
{
    public CanDrinkValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Continue;
        ClassLevelCascadeMode = CascadeMode.Continue;

        AddRule(x => x.Age).CascadeMode(CascadeMode.Stop).NotNull().Must(x => x >= 18).WithMessage("You are not old enough");
    }
}