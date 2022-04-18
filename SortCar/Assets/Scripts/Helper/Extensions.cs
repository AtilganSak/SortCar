using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    #region IEnumerable
    public static bool AddIfNo<TSource>(this List<TSource> list, TSource obj)
    {
        if (!list.Contains(obj))
        {
            list.Add(obj);
            return true;
        }
        return false;
    }
    public static Dictionary<TKey, TValue> ReverseDict<TKey, TValue>(this Dictionary<TKey, TValue> _source)
    {
        Dictionary<TKey, TValue> reversedDict = new Dictionary<TKey, TValue>();
        int sourceItemCount = _source.Count;
        for (int i = sourceItemCount - 1; i >= 0; i--)
        {
            KeyValuePair<TKey, TValue> kvp = _source.ElementAt(i);
            reversedDict.Add(kvp.Key, kvp.Value);
        }
        return reversedDict;
    }
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        Random rnd = new Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static bool DoesItHave<TSource>(this IEnumerable<TSource> source, System.Func<TSource, bool> func)
    {
        source.TryGetFirst(func, out bool found);
        return found;
    }
    public static TSource DoesItHave<TSource>(this IEnumerable<TSource> source, System.Func<TSource, bool> func, bool ovveride)
    {
        TSource first = source.TryGetFirst(func, out bool found);
        return first;
    }
    private static TSource TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
    {
        if (source == null)
        {
            throw new ArgumentNullException();
        }

        if (predicate == null)
        {
            throw new ArgumentNullException();
        }

        foreach (TSource element in source)
        {
            if (predicate(element))
            {
                found = true;
                return element;
            }
        }

        found = false;
        return default;
    }
    #endregion
}