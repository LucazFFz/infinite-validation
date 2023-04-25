namespace InfiniteValidation.Sample;

public class Order
{
    public decimal Price { get; set; }
    
    public DateTime DeliveryDate { get; set; }
    
    public decimal Weight { get; set; }
    
    public DeliveryCompany Company;
    
    public Order(decimal price, DateTime deliveryDate, DeliveryCompany company, decimal weight)
    {
        Price = price;
        DeliveryDate = deliveryDate;
        Company = company;
        Weight = weight;
    }
}