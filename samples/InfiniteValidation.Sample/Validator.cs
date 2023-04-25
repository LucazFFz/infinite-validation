using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;

namespace InfiniteValidation.Sample;

public class CustomerValidator : Validator<Customer>
{
    public CustomerValidator()
    {
        RuleForEach(x => x.Orders).Include(new OrderValidator()).Where(x => x.Company == DeliveryCompany.FedEx);

        RuleFor(x => x.Age).Must(x => x == 2);
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