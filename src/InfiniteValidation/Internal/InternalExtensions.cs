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

    internal static List<T> Replace<T>(this List<T> list, T value, int index)
    {
        list[index] = value;
        return list;
    }
    
    internal static List<T> ReplaceLast<T>(this List<T> list, T value)
    {
        var index = list.Count - 1;
        list[index] = value;
        return list;
    }
}