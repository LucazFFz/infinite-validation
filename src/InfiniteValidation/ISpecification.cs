namespace InfiniteValidation;

public interface ISpecification<T, in TProperty>
{
    public MessageBuilder MessageBuilder { get; }
    
    public bool IsSatisfiedBy(ValidationContext<T> context, TProperty value);

    public string GetName();

    public string GetMessageFormat();

    public Severity GetSeverity();
}