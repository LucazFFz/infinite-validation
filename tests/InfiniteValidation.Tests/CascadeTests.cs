using InfiniteValidation.Specifications;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class CascadeTests
{
    [Fact]
    public void Only_first_failure()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).CascadeMode(CascadeMode.Stop).Equal("foo").Must(x => x.Length >= 10);

        var name = sut.Validate(new Customer {FirstName = "bar"}).Failures.First().SpecificationName;

        Assert.Equal(new EqualSpecification<Customer, string>("foo").GetSpecificationName(), name);
    }
    
    [Fact]
    public void Only_first_failure_collection()
    {
        var sut = new TestValidator();
        sut.RuleForEach(x => x.Orders).CascadeMode(CascadeMode.Stop).Equal(null).Must(x => x.ProductName.Length >= 10);

        var name = sut.Validate(new Customer {Orders = new List<Order>
        {
            new Order
            {
                ProductName = "bar",
                Company = DeliveryCompany.UPS
            },
            
            new Order
            {
                ProductName = "foo",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.First().SpecificationName;

        Assert.Equal(new EqualSpecification<Customer, string>("foo").GetSpecificationName(), name);
    }
    
    [Fact]
    public void Only_one_failure_when_cascade_is_set_to_stop()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).CascadeMode(CascadeMode.Stop).Equal("foo").Equal("foobar");
        
        var amount = sut.Validate(new Customer {FirstName = "bar"}).Failures.Count;
        
        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void Only_one_failure_when_cascade_is_set_to_stop_collection_variant()
    {
        var sut = new TestValidator();
        sut.RuleForEach(x => x.Orders).CascadeMode(CascadeMode.Stop).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {Orders = new List<Order>
        {
            new Order
            {
                ProductName = "bar",
                Company = DeliveryCompany.UPS
            },
            
            new Order
            {
                ProductName = "foo",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void All_failures_when_cascade_is_set_to_continue_collection_variant()
    {
        var sut = new TestValidator();
        sut.RuleForEach(x => x.Orders).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {Orders = new List<Order>
        {
            new Order
            {
                ProductName = "bar",
                Company = DeliveryCompany.UPS
            },
            
            new Order
            {
                ProductName = "foo",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(2, amount);
    }
    
    [Fact]
    public void Set_DefaultRuleLevelCascadeMode_collection_variant()
    {
        var sut = new TestValidator
        {
            Configuration =
            {
                RuleLevelDefaultCascadeMode = CascadeMode.Stop
            }
        };
        sut.RuleForEach(x => x.Orders).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {Orders = new List<Order>
        {
            new Order
            {
                ProductName = "bar",
                Company = DeliveryCompany.UPS
            },
            
            new Order
            {
                ProductName = "foo",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void All_failures_when_cascade_is_set_to_continue()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).CascadeMode(CascadeMode.Continue)
            .Equal("foo")
            .Equal("foobar")
            .Must(x => x.Length >= 10);
        
        var amount = sut.Validate(new Customer {FirstName = "bar"}).Failures.Count;
        
        Assert.Equal(3, amount);
    }

    [Fact]
    public void Set_DefaultRuleLevelCascadeMode()
    {
        var sut = new TestValidator
        {
            Configuration =
            {
                RuleLevelDefaultCascadeMode = CascadeMode.Stop
            }
        };
        sut.RuleFor(x => x.FirstName).Equal("foo").Equal("foobar");
        
        var amount = sut.Validate(new Customer {FirstName = "bar"}).Failures.Count;

        Assert.Equal(1, amount);
    }

    [Fact]
    public void Only_one_failure_total_when_RuleLevelCascadeMode_and_ClassLevelCascadeMode_set_to_stop()
    {
        var sut = new TestValidator
        {
            Configuration =
            {
                RuleLevelDefaultCascadeMode = CascadeMode.Stop,
                ClassLevelDefaultCascadeMode = CascadeMode.Stop
            }
        };
        
        sut.RuleFor(x => x.FirstName).Equal("foo").Equal("foobar");
        sut.RuleForEach(x => x.Orders).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {
            FirstName = "bar",
            Orders = new List<Order> 
            {
                new Order
                {
                    ProductName = "bar",
                    Company = DeliveryCompany.UPS
                },
                
                new Order
                {
                    ProductName = "foo",
                    Company = DeliveryCompany.DHL
                }
            }
        }).Failures.Count;
        
        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void Only_one_failure_from_each_rule_when_RuleLevelCascadeMode_set_to_stop()
    {
        var sut = new TestValidator
        {
            Configuration =
            {
                RuleLevelDefaultCascadeMode = CascadeMode.Stop,
            }
        };
        
        sut.RuleFor(x => x.FirstName).Equal("foo").Equal("foobar");
        sut.RuleFor(x => x.Id).Equal(10).Equal(100);
        sut.RuleForEach(x => x.Orders).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {
            FirstName = "bar",
            Orders = new List<Order> 
            {
                new Order
                {
                    ProductName = "bar",
                    Company = DeliveryCompany.UPS
                },
                
                new Order
                {
                    ProductName = "foo",
                    Company = DeliveryCompany.DHL
                }
            }
        }).Failures.Count;
        
        Assert.Equal(3, amount);
    }
}