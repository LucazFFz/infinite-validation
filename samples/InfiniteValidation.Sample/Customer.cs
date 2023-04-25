namespace InfiniteValidation.Sample;

public class Customer
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int Age { get; set; }
    
    public List<Order> Orders { get; set; } 

    public Customer(string firstName, string lastName, int age, List<Order> orders)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Orders = orders;
    }
}