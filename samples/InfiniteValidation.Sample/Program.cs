using InfiniteValidation.Sample;

var validator = new CustomerValidator();

var person = new Customer("Sven", "Svensson", 43, new List<Order>
{
    new Order(8, new DateTime(2020, 06, 23, 05, 34, 50), DeliveryCompany.FedEx, 10), // in the past
    new Order(9, new DateTime(2023, 04, 29, 12, 04, 12), DeliveryCompany.FedEx, 1), // in the future from now
    new Order(254, new DateTime(2020, 03, 23, 14, 03, 05), DeliveryCompany.UPS, 15) // in the past
});

var result = validator.Validate(person, settings =>
{
    settings.RulesetsToValidate.Add("test");
});

Console.WriteLine(result.IsValid);