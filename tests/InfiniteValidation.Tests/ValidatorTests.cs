using InfiniteValidation.Exceptions;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class ValidatorTests
{
    [Fact]
    public void Do_not_validate_ruleset_if_key_is_not_included()
    {
        var sut = new TestValidator();

        sut.Ruleset("notIncluded", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });

        var amount = sut.Validate(new Customer()).Failures.Count;

        Assert.Equal(0, amount);
    }

    [Fact]
    public void Validate_ruleset_if_key_is_included()
    {
        var sut = new TestValidator();

        sut.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });

        var amount = sut.Validate(new Customer(), settings =>
        {
            settings.RulesetsToValidate.Add("included");
        }).Failures.Count;

        Assert.Equal(1, amount);
    }

    [Fact]
    public void Always_validate_ruleset_with_default_key()
    {
        var sut = new TestValidator();

        sut.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });

        var amount = sut.Validate(new Customer()).Failures.Count;

        Assert.Equal(1, amount);
    }

    [Fact]
    public void Throw_when_invalid_if_ThrowExceptionOnInvalid_is_true()
    {
        var sut = new TestValidator();
        
        sut.RuleFor(x => x.FirstName).Equal("bar");

        Assert.Throws<ValidationException>(() => sut.Validate(new Customer(), settings =>
        {
            settings.ThrowExceptionOnInvalid = true;
        }));
    }

    [Fact]
    public void Add_specification_to_rule()
    {
        var sut = new TestValidator();
        
        sut.RuleFor(x => x.FirstName).Equal("bar");

        var count = sut.Validate(new Customer()).Failures.Count;
        
        Assert.Equal(1, count);
    }

    [Fact]
    public void Add_specification_to_collection_rule()
    {
        var sut = new TestValidator();

        sut.RuleForEach(x => x.Orders).Must(x => x.Price == 10);

        var customer = new Customer
        {
            Orders = new List<Order>
            {
                new Order()
            }
        };
        
        var count = sut.Validate(customer).Failures.Count;
        
        Assert.Equal(1, count);
    }

    [Fact]
    public void Decorate_specification_in_rule()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).Equal("bar").WithMessage("Foo");

        var message = sut.Validate(new Customer()).Failures.First().FailureMessage;
        
        Assert.Equal("Foo", message);
    }

    [Fact]
    public void Decorate_specification_in_collection_rule()
    {
        var sut = new TestValidator();

        sut.RuleForEach(x => x.Orders).Must(x => x.Price == 10).WithMessage("Foo");

        var customer = new Customer
        {
            Orders = new List<Order>
            {
                new Order()
            }
        };
        
        var message = sut.Validate(customer).Failures.First().FailureMessage;
        
        Assert.Equal("Foo", message);
    }
    
    [Fact]
    public void Validate_all_elements_if_where_is_not_used()
    {
        var sut = new TestValidator();

        sut.RuleForEach(x => x.Orders).Must(x => x.Weight >= 10);
        
        var customer = new Customer
        {
            Orders = new List<Order>
            {
                new Order
                {
                    Price = 10,
                    Weight = 0
                },
                new Order
                {
                    Price = 20,
                    Weight = 0
                }
            }
        };
        
        var count = sut.Validate(customer).Failures.Count;
        
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void Only_validate_elements_which_fulfill_condition_using_where()
    {
        var sut = new TestValidator();

        sut.RuleForEach(x => x.Orders).Where(x => x.Price == 10).Must(x => x.Weight >= 10);

        var customer = new Customer
        {
            Orders = new List<Order>
            {
                new Order
                {
                    Price = 10,
                    Weight = 0
                },
                new Order
                {
                    Price = 20,
                    Weight = 0
                }
            }
        };

        var count = sut.Validate(customer).Failures.Count;
        
        Assert.Equal(1, count);
    }

    [Fact]
    public void Override_property_name()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).OverridePropertyName("Foo").Equal("Bar");
        
        var customer = new Customer
        {
            FirstName = "Foo"
        };

        var message = sut.Validate(customer).Failures.First().FailureMessage.Split("'");
        
        Assert.Equal("Foo", message[1]);
    }
    
    [Fact]
    public void Override_property_name_collection_variant()
    {
        var sut = new TestValidator();

        sut.RuleForEach(x => x.Orders).OverridePropertyName("Foo").Must(x => x.Price >= 10);
        
        var customer = new Customer
        {
            Orders = new List<Order>
            {
                new Order
                {
                    Price = 0
                }
            }
        };

        var message = sut.Validate(customer).Failures.First().FailureMessage.Split("'");
        
        Assert.Equal("Foo", message[1]);
    }
}