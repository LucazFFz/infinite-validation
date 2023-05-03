using InfiniteValidation.Exceptions;
using InfiniteValidation.Extensions;
using Xunit;

namespace InfiniteValidation.UnitTests.Specifications;

public class PredicateSpecificationTests
{
    [Fact]
    public void Is_valid_when_predicate_succeed()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Must(x => x == "Foo");
        sut.RuleFor(x => x.LastName).Must(x => x == null);
        
        var result = sut.Validate(new Customer {FirstName = "Foo", LastName = null});
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Is_invalid_when_predicate_does_not_succeed()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Must(x => x == "Foo");
        
        var result = sut.Validate(new Customer {FirstName = "Bar"});
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Message_use_arguments_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Must(x => x == "Foo");
        
        var msg = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().FailureMessage;
        
        Assert.Equal("'FirstName' must fulfill predicate.", msg);
    }

    [Fact]
    public void Return_correct_specification_name()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Must(x => x == "Foo");
        
        var name = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().SpecificationName;
        
        Assert.Equal("PredicateSpecification", name);
    }

    [Fact]
    public void Throw_exception_when_predicate_is_null()
    {
        var sut = new TestValidator();
        
        Assert.Throws<ArgumentNullException>(() => sut.RuleFor(x => x.FirstName).Must(null));
    }
}