﻿namespace InfiniteValidation.Decorators;

public class MessageDecorator<T, TProperty> : Decorator<T, TProperty>
{
    private readonly string _message;

    public MessageDecorator(string message)
    {
        _message = message;
    }

    public override string GetErrorMessage() => _message;
}