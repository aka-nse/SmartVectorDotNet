using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq;

internal static class EnumerableEx
{
    public static IEnumerable<(T1 first, T2 second)> Zip<T1, T2>(this IEnumerable<T1> s1, IEnumerable<T2> s2)
    {
        var e1 = s1.GetEnumerator();
        var e2 = s2.GetEnumerator();
        while(e1.MoveNext() && e2.MoveNext())
        {
            yield return (e1.Current, e2.Current);
        }
    }
}
