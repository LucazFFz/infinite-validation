using System.Linq.Expressions;
using InfiniteValidation.Results;

namespace InfiniteValidation.Internal;

internal interface IPropertyCollectionRule<T, TElement> : IPropertyRule<T, TElement>
{
    public Func<TElement, bool> ShouldValidateChildCondition { get; set; }
}