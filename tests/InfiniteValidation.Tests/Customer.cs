namespace InfiniteValidation.UnitTests;

public class Customer
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int Age { get; set; }
    
    public Gender gender { get; set; }
    
    public List<Order> Orders { get; set; }
    
    public string Email { get; set; }
    
    public string Address { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class Order
{
    public string ProductName { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTime DeliveryDate { get; set; }
    
    public decimal Weight { get; set; }
    
    public DeliveryCompany Company;
}

public enum Gender
{
    Female,
    Male
}

public enum DeliveryCompany
{
    USPS,
    UPS,
    DHL
}