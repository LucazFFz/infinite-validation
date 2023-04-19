namespace InfiniteValidation.Internal;

internal static class InternalExtensions
{
    internal static void Guard(this object? obj, string parameterName, string message)
    {
        if (obj == null) throw new ArgumentNullException(parameterName, message);
    }
    
    internal static void Guard(this object? obj, string parameterName)
    {
        if (obj == null) throw new ArgumentNullException(parameterName);
    }
}