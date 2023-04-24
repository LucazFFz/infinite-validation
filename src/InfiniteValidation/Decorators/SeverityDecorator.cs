namespace InfiniteValidation.Decorators;

public class SeverityDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Severity _severity;

    public SeverityDecorator(Severity severity)
    {
        _severity = severity;
    }

    public SeverityDecorator(ISpecification<T, TProperty> specification, Severity severity) : base(specification)
    {
        _severity = severity;
    }
    
    public override bool IsSatisfiedBy(ValidationContext<T> context, TProperty value)
        => Specification.IsSatisfiedBy(context, value);

    public override Severity GetSeverity() => _severity;
}