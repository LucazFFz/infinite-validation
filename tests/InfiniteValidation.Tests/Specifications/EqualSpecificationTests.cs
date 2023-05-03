using Xunit;

namespace InfiniteValidation.UnitTests.Specifications;

public class EqualSpecificationTests
{
    [Fact]
    public void Is_valid_when_equal()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo");
        
        var result = sut.Validate(new Customer {FirstName = "Foo"});
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Is_invalid_when_not_equal()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo");
        
        var result = sut.Validate(new Customer {FirstName = "Bar"});
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Message_use_arguments_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo");
        
        var result = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().FailureMessage;
        
        Assert.Equal("'FirstName' must equal 'Foo'.", result);
    }

    [Fact]
    public void Return_correct_specification_name()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo");
        
        var name = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().SpecificationName;
        
        Assert.Equal("EqualSpecification", name);
    }

    [Fact]
    public void Success_on_case_insensitive_comparison()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo", StringComparer.OrdinalIgnoreCase);
        
        var result = sut.Validate(new Customer {FirstName = "foo"});
        
        Assert.True(result.IsValid);
    }
}