using System.Collections.Generic;
using UnityEngine;
using Utils.ScriptableObjects.Audio;

public static class Extensions
{
    public static string JoinStr<T>(this IEnumerable<T> collection, string separator = ", ")
    {
        if (collection == null) return "<NULL>";
        return string.Join(separator, collection);
    }
    
    public static T RandomItem<T>(this IReadOnlyList<T> collection)
    {
        var idx = UnityEngine.Random.Range(0, collection.Count);
        return collection[idx];
    }

    public static float RandomWithin(this Vector2 range)
    {
        return UnityEngine.Random.Range(range.x, range.y);
    }

    public static float Random(this RangedFloat rangedFloat)
    {
        return UnityEngine.Random.Range(rangedFloat.minValue, rangedFloat.maxValue);
    }
}