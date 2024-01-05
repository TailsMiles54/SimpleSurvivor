using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class Utils
{

    public static float NormalizedValue(this float value)
    {
        return value * 0.01f;
    }
    
    public static T GetRandomElement<T>(this IList<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count-1)];
    }
    
    public static List<T> GetRandomElements<T>(this IList<T> list, int count)
    {
        var shuffledList = list.OrderBy(x => UnityEngine.Random.Range(0, 1f)).ToList();
        return shuffledList.Take(count).ToList();
    }

}
