using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Xunit;

namespace InfiniteValidation.UnitTests;

public class MessageBuilderTests
{
    [Fact]
    public void Append_argument_to_message()
    {
        var result = new MessageBuilder().Append("foo", "bar").Build("{foo}");
        Assert.Equal("bar", result);
    }

    [Fact]
    public void Append_PropertyName_to_message()
    {
        var result = new MessageBuilder().AppendPropertyName("foo").Build("{PropertyName}");
        Assert.Equal("foo", result);
    }
    
    [Fact]
    public void Append_PropertyValue_to_message()
    {
        var result = new MessageBuilder().AppendAttemptedValue("foo").Build("{PropertyValue}");
        Assert.Equal("foo", result);
    }

    [Fact]
    public void Ignore_unknown_parameters()
    {
        var now = DateTime.Now;
        var result = new MessageBuilder()
            .Append("foo", now)
            .Build("{foo:g} {unknown} {unknown:format}");
        
        Assert.Equal($"{now:g} {{unknown}} {{unknown:format}}", result);
    }

    [Fact]
    public void Ignore_unknown_numbered_parameters()
    {
        var date = new DateTime(2023, 4, 29);
        var result = new MessageBuilder()
            .Append("foo", date)
            .Build("{foo:yyyy-MM-dd} {0}");
        
        Assert.Equal("2023-04-29 {0}", result);
    }

    [Fact]
    public void Format_property_value()
    {
        string result = new MessageBuilder().AppendAttemptedValue(123.45).Build("{PropertyValue:#.#}");
        
        Assert.Equal("123.5", result);
    }

    [Fact]
    public void Understand_date_formats()
    {
        var now = DateTime.Now;
        var result = new MessageBuilder()
            .Append("now", now)
            .Build("{now:g} {now:MM-dd-yy} {now:f}");
        
        Assert.Equal($"{now:g} {now:MM-dd-yy} {now:f}", result);
    }
}