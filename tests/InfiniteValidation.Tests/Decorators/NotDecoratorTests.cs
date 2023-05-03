using InfiniteValidation.Specifications;
using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class NotDecoratorTests
{
    [Fact]
    public void Is_valid_when_specification_is_invalid()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Not();

        var result = sut.Validate(new Customer {FirstName = "Bar"}).IsValid;
        
        Assert.True(result);
    }
    
    [Fact]
    public void Is_invalid_when_specification_is_valid()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Not();

        var result = sut.Validate(new Customer {FirstName = "Foo"}).IsValid;
        
        Assert.False(result);
    }
}