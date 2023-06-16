using System.Text.RegularExpressions;
using Xunit;
using InfiniteValidation.Extensions;

namespace InfiniteValidation.UnitTests;

public class ExtensionTests
{
    [Fact]
    public void Return_false_if_object_does_not_fulfill_predicate()
    {
        var name = "Foo";
        var result = name.Fulfills(x => x == "Bar");

        Assert.False(result);
    }
    
    [Fact]
    public void Return_true_if_object_fulfills_predicate()
    {
        var name = "Foo";
        var result = name.Fulfills(x => x == "Foo");

        Assert.True(result);
    }

    [Fact]
    public void Return_false_if_string_does_not_match_regex()
    {
        var name = "Bar";
        var result = name.Matches(new Regex("^Foo"));
        
        Assert.False(result);
    }
    
    [Fact]
    public void Return_true_if_string_matches_regex()
    {
        var name = "Foo";
        var result = name.Matches(new Regex("^Foo"));
        
        Assert.True(result);
    }
}