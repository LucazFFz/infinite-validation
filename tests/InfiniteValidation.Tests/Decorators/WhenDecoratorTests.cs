using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class WhenDecoratorTests
{
    [Fact]
    public void Is_valid_when_condition_is_false()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").When(x => x.Length > 1);

        var actual = sut.Validate(new Customer {FirstName = "b"}).IsValid;
        
        Assert.True(actual);
    }

    [Fact]
    public void Is_valid_when_specification_and_condition_are_false()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").When(x => x.Length >= 3);

        var actual = sut.Validate(new Customer {FirstName = "b"}).IsValid;
        
        Assert.True(actual);
    }
    
    [Fact]
    public void Is_valid_when_specification_and_condition_are_true()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").When(x => x.Length > 1);

        var actual = sut.Validate(new Customer {FirstName = "Foo"}).IsValid;
        
        Assert.True(actual);
    }
    
    [Fact]
    public void Is_invalid_when_specification_is_invalid_and_condition_is_valid()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").When(x => x.Length >= 1);

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).IsValid;
        
        Assert.False(actual);
    }

    [Fact]
    public void Throw_exception_when_condition_is_null()
    {
        var sut = new TestValidator();
        
        Assert.Throws<ArgumentNullException>(() =>
            sut.RuleFor(x => x.FirstName).Equal("Foo").When(null));
    }
}