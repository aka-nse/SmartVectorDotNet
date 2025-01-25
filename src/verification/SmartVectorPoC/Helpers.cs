// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
// ScalarOp_Population.TestPopulation();

internal static class Helpers
{
    public static string ToString<T>(this Vector<T> vector, string format)
        where T : unmanaged, IFormattable
        => vector.ToString(format, null);

    public static string ToString<T>(this Vector<T> vector, string format, IFormatProvider? formatProvider)
        where T : unmanaged, IFormattable
        => Unsafe.As<Vector<T>, Vector256<T>>(ref vector).ToString(format, formatProvider);

    public static string ToString<T>(this Vector256<T> vector, string format)
        where T : unmanaged, IFormattable
        => vector.ToString(format, null);

    public static string ToString<T>(this Vector256<T> vector, string format, IFormatProvider? formatProvider)
        where T : unmanaged, IFormattable
    {
        var sb = new StringBuilder();
        sb.Append('<');
        for (var i = 0; i < Vector256<T>.Count; i++)
        {
            sb.Append(vector[i].ToString(format, formatProvider));
            if (i < Vector256<T>.Count - 1)
            {
                sb.Append(", ");
            }
        }
        sb.Append('>');
        return sb.ToString();
    }
}