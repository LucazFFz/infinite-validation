namespace InfiniteValidation;

public sealed class ValidationContext<T>
{
    public T InstanceToValidate { get; }

    public ValidationSettings Settings { get; } 

    public ValidationContext(T instanceToValidate)
    {
        InstanceToValidate = instanceToValidate;
        Settings = new ValidationSettings();
    }
    
    public ValidationContext(T instanceToValidate, ValidationSettings settings)
    {
        Settings = settings;
        InstanceToValidate = instanceToValidate;
    }
}