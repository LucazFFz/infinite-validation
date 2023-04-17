using InfiniteValidation.Models;

namespace InfiniteValidation.Specifications;

public class NullSpecification<T, TProperty> : AbstractSpecification<T, TProperty>
{ 
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => value is null;
    
    public override string GetSpecificationName() => "Null";
    
    public override string GetErrorMessage() => "this is a test message";
}