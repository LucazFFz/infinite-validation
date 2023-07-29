namespace InfiniteValidation.Specifications;

public sealed class NullSpecification<T, TProperty> : Specification<T, TProperty>
{ 
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => value is null;
    
    public override string GetName() => "NullSpecification";
    
    public override string GetMessageFormat() => "'{PropertyName}' must equal 'null'.";
}