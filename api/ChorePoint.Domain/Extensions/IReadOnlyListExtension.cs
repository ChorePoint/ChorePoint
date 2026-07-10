namespace ChorePoint.Domain.Extensions;

public static class IReadOnlyListExtension
{
    public static bool Empty<T>(this IReadOnlyList<T>? list)
    {
        return list is not null && list.Count == 0;
    }
}