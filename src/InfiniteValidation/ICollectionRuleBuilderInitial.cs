using InfiniteValidation.Internal;

namespace InfiniteValidation;

public interface ICollectionRuleBuilderInitial<T, TElement> : IRuleBuilder<T, TElement>
{
    public ICollectionRuleBuilderInitial<T, TElement> CascadeMode(CascadeMode mode);
}