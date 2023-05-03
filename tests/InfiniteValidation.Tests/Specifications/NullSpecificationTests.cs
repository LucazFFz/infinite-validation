using Xunit;

namespace InfiniteValidation.UnitTests.Specifications;

public class NullSpecificationTests
{
    [Fact]
    public void Is_valid_when_equal_null()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Null();
        
        var result = sut.Validate(new Customer {FirstName = null});
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Is_invalid_when_not_equal_null()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Null();
        
        var result = sut.Validate(new Customer {FirstName = "Bar"});
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Message_use_arguments_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Null();
        
        var msg = sut.Validate(new Customer {FirstName = "Foo"}).Failures.First().FailureMessage;
        
        Assert.Equal("'FirstName' must equal 'null'.", msg);
    }

    [Fact]
    public void Return_correct_specification_name()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Null();
        
        var name = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().SpecificationName;
        
        Assert.Equal("NullSpecification", name);
    }

    
}