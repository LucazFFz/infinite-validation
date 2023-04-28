using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

public class CollectionRule<T, TElement> : IPropertyCollectionRule<T, TElement>, IValidatorRule<T>
{
    public string PropertyName { get; set; }
    
    public IValidator<TElement>? ChildValidator { get; set; } 

    public Func<TElement, bool> ShouldValidateChildCondition { get; set; } = _ => true;
    
    public Expression<Func<T, IEnumerable<TElement>>> Expression { get; }
    
    public CascadeMode CascadeMode { get; set; }

    public List<ISpecification<T, TElement>> Specifications { get; set; } = new();
    
    public CollectionRule(Expression<Func<T, IEnumerable<TElement>>> expression, CascadeMode cascadeMode, string propertyName)
    { 
        CascadeMode = cascadeMode;
        Expression = expression;
        PropertyName = propertyName;
    }

    public IEnumerable<ValidationFailure> IsValid(ValidationContext<T> context)
    {
        var failures = new List<ValidationFailure>();
        var instance = Expression.Compile()(context.InstanceToValidate);

        foreach (var property in instance)
        {
            if(!ShouldValidateChildCondition.Invoke(property)) continue;
            if(ChildValidator != null) failures.AddRange(ChildValidator.Validate(property, context.Settings).Errors);
            
            foreach (var specification in Specifications
                .Where(specification => !specification.IsSatisfiedBy(context, property)))
            {
                specification.MessageBuilder.AppendPropertyName(PropertyName).AppendAttemptedValue(property!);
                failures.Add(ValidationFailureFactory.Create(specification, property));
                if (CascadeMode == CascadeMode.Stop) break;
            }
        }

        return failures;
    }
}