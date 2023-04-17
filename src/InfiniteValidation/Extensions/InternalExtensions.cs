namespace InfiniteValidation.Extensions;

internal static class InternalExtensions
{
    internal static void Guard(this object obj, string message, string parameterName)
    {
        if (obj == null) throw new ArgumentNullException(parameterName, message);
    }
}