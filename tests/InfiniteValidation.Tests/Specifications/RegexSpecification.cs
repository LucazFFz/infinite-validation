using System.Text.RegularExpressions;
using Xunit;

namespace InfiniteValidation.UnitTests.Specifications;

public class RegexSpecification
{
    [Fact]
    public void Is_valid_when_match_regex()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Match(@"([A-Z])\w+");
        sut.RuleFor(x => x.FirstName).Match(new Regex(@"([A-Z])\w+"));

        var result = sut.Validate(new Customer {FirstName = "Foo"});
        
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Is_invalid_when_does_not_match_regex()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Match(@"([A-Z])\w+");
        
        var result = sut.Validate(new Customer {FirstName = "foo"});
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Message_use_arguments_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Match(@"([A-Z])\w+");
        
        var msg = sut.Validate(new Customer {FirstName = "foo"}).Failures.First().FailureMessage;
        
        Assert.Equal(@"'FirstName' must match regex: '([A-Z])\w+'.", msg);
    }

    [Fact]
    public void Return_correct_specification_name()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Match(@"([A-Z])\w+");
        
        var name = sut.Validate(new Customer {FirstName = "foo"}).Failures.First().SpecificationName;
        
        Assert.Equal("RegexSpecification", name);
    }

    [Fact]
    public void Match_with_regex_options()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Match(@"([A-Z])\w+", RegexOptions.IgnoreCase);
        sut.RuleFor(x => x.FirstName).Match(new Regex(@"([A-Z])\w+", RegexOptions.IgnoreCase));

        var result = sut.Validate(new Customer {FirstName = "foo"});
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Throw_exception_when_regex_is_null()
    {
        var sut = new TestValidator();
        string regex = null;
        
        Assert.Throws<ArgumentNullException>(() => sut.RuleFor(x => x.FirstName).Match(regex));
    }
}