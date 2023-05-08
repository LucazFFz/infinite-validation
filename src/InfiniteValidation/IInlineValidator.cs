using System.Linq.Expressions;

namespace InfiniteValidation;

public interface IInlineValidator<T>
{
    public IRuleBuilderInitial<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> expression);

    public ICollectionRuleBuilderInitial<T, TElement> RuleForEach<TElement>(
        Expression<Func<T, IEnumerable<TElement>>> expression);
}