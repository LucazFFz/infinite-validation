using System.Linq.Expressions;
using InfiniteValidation.Models;

namespace InfiniteValidation.Internal;

internal class Rule<T, TProperty> : IRule<T, TProperty>
{
    private readonly Expression<Func<T, TProperty>> _expression;

    private readonly List<ISpecification<T, TProperty>> _specifications = new();

    public Rule(Expression<Func<T, TProperty>> expression)
    {
        _expression = expression;
    }
    
    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var value = _expression.Compile()(context.InstanceToValidate);
        return (from specification in _specifications 
            where !specification.IsSatisfiedBy(context, value) 
            select specification.GetValidationFailure(value)).ToList();
    }

    public void AddSpecification(ISpecification<T, TProperty> specification) => _specifications.Add(specification);

    public List<ISpecification<T, TProperty>> GetSpecifications() => _specifications;
}