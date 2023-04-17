using System.Text.RegularExpressions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class EmailSpecification<T> : AbstractSpecification<T, string>
{
    private readonly Regex _regex 
        = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, string value) => _regex.IsMatch(value);
    
    public override string GetSpecificationName() => "EmailAddress";
    
    public override string GetErrorMessage() => "this is a test message";
}