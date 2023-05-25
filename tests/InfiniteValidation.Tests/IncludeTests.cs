using Xunit;

namespace InfiniteValidation.UnitTests;

public class IncludeTests
{

    [Fact]
    public void Include_all_rules_from_validator_rulesets_in_rule()
    {
        var includedValidator = new InlineValidator<string>();
        var sut = new TestValidator();
        
        includedValidator.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.Length).Equal(10);
            validator.RuleFor(x => x).Equal("foo");
        });
        
        sut.RuleFor(x => x.FirstName).Include(includedValidator);

        var amount = sut.Validate(new Customer
        {
            FirstName = "bar"
        }).Failures.Count;
        
        Assert.Equal(2, amount);
    }
    
    
    [Fact]
    public void Validate_included_rulesets_in_rule_independent_of_their_key()
    {
        var includedValidator = new InlineValidator<string>();
        var sut = new TestValidator();
        
        includedValidator.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.Length).Equal(10);
        });
        
        includedValidator.Ruleset("notIncluded", validator =>
        {
            validator.RuleFor(x => x).Equal("foo");
        });

        sut.RuleFor(x => x.FirstName).Include(includedValidator);

        var amount = sut.Validate(new Customer
        {
            FirstName = "bar"
        }).Failures.Count;
        
        Assert.Equal(2, amount);
    }

    [Fact]
    public void Throw_error_if_validator_parameter_is_null()
    {
        var sut = new TestValidator();

        Assert.Throws<ArgumentNullException>(() =>
        {
            sut.RuleFor(x => x).Include(null);
        });
    }
    
    [Fact]
    public void Include_all_rules_from_validator_rulesets_in_collection_rule()
    {
        var includedValidator = new InlineValidator<Order>();
        var sut = new TestValidator();
        
        includedValidator.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.Price).Equal(10);
            validator.RuleFor(x => x.ProductName).Equal("foo");
        });
        
        sut.RuleForEach(x => x.Orders).Include(includedValidator);

        var amount = sut.Validate(new Customer
        {
            FirstName = "bar",
            Orders = new List<Order>
            {
                new Order
                {
                    ProductName = "bar",
                    Price = 0
                }
            }
        }).Failures.Count;
        
        Assert.Equal(2, amount);
    }
    
    [Fact]
    public void Validate_included_rulesets_in_collection_rule_independent_of_their_key()
    {
        var includedValidator = new InlineValidator<Order>();
        var sut = new TestValidator();
        
        includedValidator.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.Price).Equal(10);
        });
        
        includedValidator.Ruleset("notIncluded", validator =>
        {
            validator.RuleFor(x => x.ProductName).Equal("foo");
        });

        sut.RuleForEach(x => x.Orders).Include(includedValidator);

        var amount = sut.Validate(new Customer
        {
            FirstName = "bar",
            Orders = new List<Order>
            {
                new Order
                {
                    ProductName = "bar",
                    Price = 0
                }
            }
        }).Failures.Count;
        
        Assert.Equal(2, amount);
    }
    
    [Fact]
    public void Throw_error_if_validator_parameter_is_null_collection_variant()
    {
        var sut = new TestValidator();
        InlineValidator<Order>? includeValidator = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            sut.RuleForEach(x => x.Orders).Include(includeValidator!);
        });
    }

    [Fact]
    public void Throw_error_if_validator_parameter_is_null_in_class_level_include()
    {
        var sut = new TestValidator();
        
        Assert.Throws<ArgumentNullException>(() =>
        {
            sut.Include(null!);
        });
    }
    
    [Fact]
    public void Include_all_rulesets_from_validator()
    {
        var sut = new TestValidator();
        var includedValidator = new TestValidator();

        includedValidator.RuleFor(x => x.Age).Equal(10);
        includedValidator.Ruleset(Validator<object>.DefaultRulesetKey, validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });
        includedValidator.RuleFor(x => x.Email).Equal("foo");
        
        sut.Include(includedValidator);
        
        var amount = sut.Validate(new Customer()).Failures.Count;

        Assert.Equal(3, amount);
    }

    [Fact]
    public void Validate_if_the_included_rulesets_key_has_been_added_to_the_list_of_rulesets_to_validate()
    {
        var sut = new TestValidator();
        var includedValidator = new TestValidator();
        
        includedValidator.Ruleset("included", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });
        
        sut.Include(includedValidator);
        
        var amount = sut.Validate(new Customer(), settings =>
        {
            settings.RulesetsToValidate.Add("included");
        }).Failures.Count;

        Assert.Equal(1, amount);
    }
    
    [Fact]
    public void Do_not_validate_if_the_included_rulesets_key_has_not_been_added_to_the_list_of_rulesets_to_validate()
    {
        var sut = new TestValidator();
        var includedValidator = new TestValidator();
        
        includedValidator.Ruleset("notIncluded", validator =>
        {
            validator.RuleFor(x => x.FirstName).Equal("bar");
        });
        
        sut.Include(includedValidator);
        
        var amount = sut.Validate(new Customer()).Failures.Count;

        Assert.Equal(0, amount);
    }
}