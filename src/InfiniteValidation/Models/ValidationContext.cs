namespace InfiniteValidation.Models;

public class ValidationContext<T>
{
    public T  InstanceToValidate { get; }

    public ValidationContext(T instanceToValidate)
    {
        InstanceToValidate = instanceToValidate;
    }
}