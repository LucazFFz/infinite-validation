using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class SpecificationNameDecoratorTests
{
    [Fact]
    public void Set_specification_name_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").WithSpecificationName("Foo");

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().SpecificationName;
        
        Assert.Equal("Foo", actual);
    }
}