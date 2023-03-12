using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    // Resource obtained from https://gist.github.com/oviniciusfaria/acbc85d0f49af2e2d02e9de1a479e3bb
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
