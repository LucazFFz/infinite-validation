using Xunit;

namespace InfiniteValidation.UnitTests.Decorators;

public class SeverityDecoratorTest
{
    [Fact]
    public void Set_severity_correctly()
    {
        var sut = new TestValidator();
        sut.RuleFor(x => x.FirstName).Equal("Foo").WithSeverity(Severity.Info);

        var actual = sut.Validate(new Customer {FirstName = "Bar"}).Failures.First().Severity;
        
        Assert.Equal(Severity.Info, actual);
    }
}