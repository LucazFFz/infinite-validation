using InfiniteValidation.RuleSetDecorators;

namespace InfiniteValidation.Sample;

public class CustomerValidator : Validator<Customer>
{
    public CustomerValidator()
    {
        this.RuleSet(DefaultRuleSetName, validator =>
        {
            validator.RuleFor(x => x.Age).Equal(10);
            validator.RuleFor(x => x.FirstName).Equal("hejsan");
        });
    }
}

public class OrderValidator : Validator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.Price).Must(x => x >= 10).WithMessage("Order is too cheap");
        RuleFor(x => x.DeliveryDate).Must(x => x > DateTime.Now).WithMessage("Order has been delivered");
        RuleFor(x => x.Weight).Must(x => x <= 10).WithSeverity(Severity.Info).WithMessage("Order is heavy");
    }
}

public class PersonValidator : Validator<Customer>
{
    public PersonValidator()
    {
        this.RuleSet(DefaultRuleSetName, validator =>
        {

        });
    }
}