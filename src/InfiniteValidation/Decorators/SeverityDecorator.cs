namespace InfiniteValidation.Decorators;

public class SeverityDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly Severity _severity;

    public SeverityDecorator(Severity severity)
    {
        _severity = severity;
    }

    public override Severity GetSeverity() => _severity;
}