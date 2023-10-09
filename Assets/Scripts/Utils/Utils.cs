using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

}
