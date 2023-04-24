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

    internal static void Replace<T>(this List<T> list, T value, int index)
    {
        list[index] = value;
    }
    
    internal static void ReplaceLast<T>(this List<T> list, T value)
    {
        var index = list.Count - 1;
        list[index] = value;
    }
}