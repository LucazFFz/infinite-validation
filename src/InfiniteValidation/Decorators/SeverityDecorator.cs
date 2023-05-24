namespace InfiniteValidation.Decorators;

public class SeverityDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Severity _severity;

    public SeverityDecorator(ISpecification<T, TProperty> specification, Severity severity) : base(specification)
    {
        _severity = severity;
    }

    public override Severity GetSeverity() => _severity;
}