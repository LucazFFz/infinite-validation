namespace InfiniteValidation.Decorators;

public class MessageDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly string _messageFormat;

    public MessageDecorator(string messageFormat)
    {
        _messageFormat = messageFormat;
    }

    public override string GetMessageFormat() => _messageFormat;
}