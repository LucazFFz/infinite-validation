using InfiniteValidation.Decorators;
using InfiniteValidation.Specifications;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class DecoratorTests
{
    [Fact]
    public void Pass_trough_specification_IsSatisfied_method()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).Equal("Foo").WithMessage("FizzBuzz");

        var actual = sut.Validate(new Customer {FirstName = "Foo"}).IsValid;
        
        Assert.True(actual);
    }
    
    [Fact]
    public void Pass_trough_specification_specification_mame()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).Equal("Foo").WithMessage("FizzBuzz");

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().SpecificationName;
        
        Assert.Equal("EqualSpecification", actual);
    }
    
    [Fact]
    public void Pass_trough_specification_severity()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).Equal("Foo").WithMessage("FizzBuzz");

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().Severity;
        
        Assert.Equal(Severity.Error, actual);
    }
    
    [Fact]
    public void Pass_trough_specification_error_message()
    {
        var sut = new TestValidator();

        sut.RuleFor(x => x.FirstName).Equal("Foo").WithSeverity(Severity.Error);

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().FailureMessage;
        
        Assert.Equal("'FirstName' must equal 'Foo'.", actual);
    }
    
    /*
    [Fact]
    public void Update_message_builder_when_setting_specification()
    {
        var sut = new TestValidator();
        var decorator = new MessageDecorator<Customer, string>("{ComparisonValue}")
        {
            Specification = new NullSpecification<Customer, string>()
        };
        sut.RuleFor(x => x.FirstName).Specify(decorator);

        decorator.Specification = new EqualSpecification<Customer, string>("Foo");

        var message = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().FailureMessage;
        
        Assert.Equal("Foo", message);
    }
    */

    [Fact]
    public void Throw_exception_when_setting_specification_to_null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            new MessageDecorator<Customer, string>(null, "{ComparisonValue}");
        });
    }
    
}