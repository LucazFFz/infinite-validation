using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Sample;

public class CanDrinkValidator : Validator<Person>
{
    public CanDrinkValidator()
    {
        Configuration.RuleLevelDefaultCascadeMode = CascadeMode.Continue;
        Configuration.ClassLevelDefaultCascadeMode = CascadeMode.Continue;

        RuleFor(x => x.Age).CascadeMode(CascadeMode.Stop).Null().Not().Must(x => x >= 18).WithMessage("You are not old enough");
    }
}