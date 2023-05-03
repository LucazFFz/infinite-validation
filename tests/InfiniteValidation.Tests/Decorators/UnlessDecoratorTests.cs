using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class UnlessDecoratorTests
{
    [Fact]
    public void Is_valid_when_condition_is_true_and_specification_is_false()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Unless(x => x.Length >= 3);

        var actual = sut.Validate(new Customer {FirstName = "FizzBuzz"}).IsValid;
        
        Assert.True(actual);
    }

    [Fact]
    public void Is_invalid_when_specification_and_condition_are_false()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Unless(x => x.Length >= 3);

        var actual = sut.Validate(new Customer {FirstName = "b"}).IsValid;
        
        Assert.False(actual);
    }
    
    [Fact]
    public void Is_valid_when_specification_and_condition_are_true()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Unless(x => x.Length > 1);

        var actual = sut.Validate(new Customer {FirstName = "Foo"}).IsValid;
        
        Assert.True(actual);
    }
    
    [Fact]
    public void Is_valid_when_specification_is_true_and_condition_is_false()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").Unless(x => x.Length >= 10);

        var actual = sut.Validate(new Customer {FirstName = "Foo"}).IsValid;
        
        Assert.True(actual);
    }

    [Fact]
    public void Throw_exception_when_condition_is_null()
    {
        var sut = new TestValidator();
        
        Assert.Throws<ArgumentNullException>(() =>
            sut.RuleFor(x => x.FirstName).Equal("Foo").Unless(null));
    }
}