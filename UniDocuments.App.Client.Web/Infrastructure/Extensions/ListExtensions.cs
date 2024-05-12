namespace UniDocuments.App.Client.Web.Infrastructure.Extensions;

public static class ListExtensions
{
    public static List<T> With<T>(this List<T> source, IEnumerable<T> data)
    {
        source.AddRange(data);
        return source;
    }

    public static void RemoveLast<T>(this IList<T> source)
    {
        source.RemoveAt(source.Count - 1);
    }
}