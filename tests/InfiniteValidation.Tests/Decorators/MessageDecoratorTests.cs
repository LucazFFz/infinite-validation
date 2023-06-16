using InfiniteValidation.Specifications;
using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class MessageDecoratorTests
{
    private readonly TestValidator _validator = new();
    
    [Theory]
    [InlineData("FooBar")]
    [InlineData("")]
    public void Use_decorator_error_message(string msg)
    {
        _validator.RuleFor(x => x.Id).Equal(10).WithMessage(msg);
        var actual = _validator.Validate(new Customer()).Failures.First().FailureMessage;
        
        Assert.Equal(msg, actual);
    }

    [Fact]
    public void Use_last_set_error_message()
    {
        _validator.RuleFor(x => x.Id).Equal(10).WithMessage("1").WithMessage("2");
        var actual = _validator.Validate(new Customer()).Failures.First().FailureMessage;
        
        Assert.Equal("2", actual);
    }

    [Fact]
    public void Throw_exception_if_message_is_null()
    {
        _validator.RuleFor(x => x.Id).Equal(10).WithMessage(null);

        Assert.Throws<ArgumentNullException>(() => _validator.Validate(new Customer()));
    }
    
    [Fact]
    public void Use_specification_specific_message_argument_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName.Length).Equal(10).WithMessage("{ComparisonValue} is not correct length");

        var result = sut.Validate(new Customer {FirstName = "Foo"}).Failures.First().FailureMessage;
        
        Assert.Equal("10 is not correct length", result);
    }
}