﻿namespace InfiniteValidation.Specifications;

public class NullSpecification<T, TProperty> : Specification<T, TProperty>
{ 
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value) => value is null;
    
    public override string GetSpecificationName() => "NullSpecification";
    
    public override string GetMessageFormat() => "'{PropertyName}' must equal 'null'.";
}