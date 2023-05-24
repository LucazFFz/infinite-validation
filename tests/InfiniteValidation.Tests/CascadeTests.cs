using InfiniteValidation.Extensions;
using InfiniteValidation.Specifications;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class CascadeTests
{
    [Fact]
    public void Only_one_failure_when_cascade_is_set_to_stop()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).Equal("foo").Equal("foobar");
        
        var amount = sut.Validate(new Customer {FirstName = "bar"}).Failures.Count;
        
        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void Only_check_first_specification_but_still_for_every_element_when_cascade_is_set_to_stop_collection_variant()
    {
        var sut = new TestValidator();
        sut.RuleForEach(x => x.Orders).Cascade(CascadeMode.Stop).Must(x => x.ProductName == "foo").Must(x => x.Company == DeliveryCompany.UPS);
        
        var amount = sut.Validate(new Customer {Orders = new List<Order>
        {
            new Order
            {
                ProductName = "bar",
                Company = DeliveryCompany.DHL
            },
            
            new Order
            {
                ProductName = "foobar",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(2, amount);
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
                Company = DeliveryCompany.DHL
            },
            
            new Order
            {
                ProductName = "foobar",
                Company = DeliveryCompany.DHL
            },
            
            new Order
            {
                ProductName = "foo",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(5, amount);
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
                Company = DeliveryCompany.DHL
            },
            
            new Order
            {
                ProductName = "foobar",
                Company = DeliveryCompany.DHL
            }
        }}).Failures.Count;
        
        Assert.Equal(2, amount);
    }

    [Fact]
    public void All_failures_when_cascade_is_set_to_continue()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Cascade(CascadeMode.Continue)
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
                    Company = DeliveryCompany.DHL
                },
                
                new Order
                {
                    ProductName = "foobar",
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
                    Company = DeliveryCompany.DHL
                },
                
                new Order
                {
                    ProductName = "foobar",
                    Company = DeliveryCompany.DHL
                }
            }
        }).Failures.Count;
        
        Assert.Equal(4, amount);
    }

    [Fact]
    public void Only_validate_first_rule_in_first_used_ruleset_when_ClassLevelCascadeMode_set_to_stop()
    {
        var customer = new Customer
        {
            FirstName = "foobar",
            LastName = "foobar"
        };
        
        var sut = new TestValidator
        {
            Configuration =
            {
                ClassLevelDefaultCascadeMode = CascadeMode.Stop,
            }
        };

        sut.Ruleset("notIncluded", validator =>
        {
            validator.RuleFor(x => x.LastName).Equal("foo");
        });
        
        sut.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("foo").Must(x => x.Length == 3);
            validator.RuleFor(x => x.LastName).Equal("foo");
        });

        sut.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("foo").Must(x => x.Length == 3);
            validator.RuleFor(x => x.LastName).Equal("foo");
        });
        
        var amount =  sut.Validate(customer, settings =>
        {
            settings.RuleSetsToValidate.Add("included");
        }).Failures.Count;
        
        Assert.Equal(2, amount);
    }

    [Fact]
    public void All_failures_from_different_rulesets_when_cascade_is_set_to_continue()
    {
        var customer = new Customer
        {
            FirstName = "foobar",
            LastName = "foobar",
            Orders = new List<Order> 
            {
                new Order
                {
                    ProductName = "bar",
                    Company = DeliveryCompany.DHL
                },
                
                new Order
                {
                    ProductName = "foobar",
                    Company = DeliveryCompany.DHL
                }
            }
        };

        var sut = new TestValidator();
        
        sut.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("foo").Must(x => x.Length == 3);
            validator.RuleFor(x => x.LastName).Equal("foo");
        });

        sut.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("foo").Must(x => x.Length == 3);
            validator.RuleFor(x => x.LastName).Equal("foo");
            validator.RuleForEach(x => x.Orders).Must(x => x.ProductName == "foo");
        });
        
        var amount =  sut.Validate(customer, settings =>
        {
            settings.RuleSetsToValidate.Add("included");
        }).Failures.Count;
        
        Assert.Equal(8, amount);
    }
    
    
}