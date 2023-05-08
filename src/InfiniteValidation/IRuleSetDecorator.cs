namespace InfiniteValidation;

public interface IRuleSetDecorator<T> : IRuleSet<T>
{
    public IRuleSet<T> RuleSet { get; set; }
}