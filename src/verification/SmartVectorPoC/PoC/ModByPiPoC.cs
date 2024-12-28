using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet.PoC;

internal static class ModByPiPoC
{
    public static void Test()
    {
        // TryCurrentImplement(1.6974669e+37f, "0.70455354449120667536127493266477339468833395837477139294799534593363841979...");
        // TryCurrentImplementP2(1.2366686e+30f, $"{(float)Math.Asin(Math.Sin(1.2366686e+30f))}");
        
        var riskyX = SearchForSmallModTau();
        foreach(var x in riskyX)
        {
            var sin = Math.Sin((double)x);
            // TryCurrentImplementP2(x, $"{(float)Math.Asin(sin):E08}");
            TryCurrentImplementN(x, $"{(float)Math.Asin(sin):E08}");
        }
    }


    private static IEnumerable<float> SearchForSmallModTau()
    {
        var x = 1e+30f;
        var min = float.PositiveInfinity;
        while (!float.IsNaN(x) && !float.IsInfinity(x))
        {
            var cos = Math.Cos((double)x);
            var sin = Math.Sin((double)x);
            var dist_1_0 = (cos - 1) * (cos - 1) + sin * sin;
            if (dist_1_0 < min && sin >= 0)
            {
                min = (float)dist_1_0;
                yield return x;
            }
            ScalarMath.Decompose(x, out int sign_, out int expo_, out int frac_);
            ++frac_;
            if (frac_ >= 0x800000)
            {
                frac_ &= ~0x7F800000;
                ++expo_;
            }
            x = ScalarMath.Scale(sign_, expo_, frac_);
        }
    }


    private static void TryCurrentImplementP2(float x, string? expected = null)
    {
        ScalarMath.Decompose(x, out int _, out int expo_, out int frac_);
        Console.WriteLine($"target: {x}  ({0x800000 | frac_} * 2^{expo_ - 127 -23})");
        Console.WriteLine($"exp part: {expo_:X02}");
        Console.WriteLine($"frac part: {frac_:X06}");
        var x1 = ScalarMathPoC.ModByPiP2_Fixed(x);
        var x2 = ScalarMathPoC.ModByPiP2(x);
        Console.WriteLine($"x % pi/2 calculated fixed   : {x1:E08}  [sin(x) = {MathF.Sin(x1):E08}, cos(x) = {MathF.Cos(x1):E08}]");
        Console.WriteLine($"x % pi/2 calculated floating: {x2:E08}  [sin(x) = {MathF.Sin(x2):E08}, cos(x) = {MathF.Cos(x2):E08}]");
        if (expected is { })
        {
            Console.WriteLine($"x % pi/2 expected           : {expected}");
        }
        Console.WriteLine();
    }

    private static void TryCurrentImplementN(float x, string? expected = null)
    {
        ScalarMath.Decompose(x, out int _, out int expo_, out int frac_);
        Console.WriteLine($"target: {x}  ({0x800000 | frac_} * 2^{expo_ - 127 - 23})");
        Console.WriteLine($"exp part: {expo_:X02}");
        Console.WriteLine($"frac part: {frac_:X06}");
        var y = ScalarMathPoC.ModByPiN(x, 1);
        Console.WriteLine($"x % 2pi calculated floating: {y:E08}  [sin(x) = {MathF.Sin(y):E08}, cos(x) = {MathF.Cos(y):E08}]");
        if (expected is { })
        {
            Console.WriteLine($"x % 2pi expected           : {expected}");
        }
        Console.WriteLine();
    }
}


file static class ScalarMathPoC
{
    internal static readonly uint[] ipip2 = [
        0xA2, 0xF9, 0x83, 0x6E, 0x4E, 0x44, 0x15, 0x29,
        0xFC, 0x27, 0x57, 0xD1, 0xF5, 0x34, 0xDD, 0xC0,
        0xDB, 0x62, 0x95, 0x99, 0x3C, 0x43, 0x90, 0x41,
        0xFE, 0x51, 0x63, 0xAB, 0xDE, 0xBB, 0xC5, 0x61,
        0xB7, 0x24, 0x6E, 0x3A, 0x42, 0x4D, 0xD2, 0xE0,
        0x06, 0x49, 0x2E, 0xEA, 0x09, 0xD1, 0x92, 0x1C,
        0xFE, 0x1D, 0xEB, 0x1C, 0xB1, 0x29, 0xA7, 0x3E,
        0xE8, 0x82, 0x35, 0xF5, 0x2E, 0xBB, 0x44, 0x84,
        0xE9, 0x9C, 0x70, 0x26, 0xB4, 0x5F, 0x7E, 0x41,
        0x39, 0x91, 0xD6, 0x39, 0x83, 0x53, 0x39, 0xF4,
        0x9C, 0x84, 0x5F, 0x8B, 0xBD, 0xF9, 0x28, 0x3B,
        0x1F, 0xF8,
    ];

    internal static uint ShiftBoth(uint x, int y)
        => y > 0
            ? x << y
            : x >> -y;

    internal static ulong ShiftBoth(ulong x, int y)
        => y > 0
            ? x << y
            : x >> -y;

    public static float ModByPiP2_Fixed(float x)
    {
        ScalarMath.Decompose(x, out var sign, out var expo_, out var frac_);
        var frac = ((uint)frac_) | 0x800000;
        int expo, start;
        expo = expo_ - 127 - 23;
        start = (expo + 7) / 8;

        var zz =
            (+ShiftBoth(frac * ipip2[start + (0 - 1)], expo - (start + 0) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (1 - 1)], expo - (start + 1) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (2 - 1)], expo - (start + 2) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (3 - 1)], expo - (start + 3) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (4 - 1)], expo - (start + 4) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (5 - 1)], expo - (start + 5) * 8 + 32)
            + ShiftBoth(frac * ipip2[start + (6 - 1)], expo - (start + 6) * 8 + 32)
        );
        var z = zz * MathF.Pow(2, -32);
        return (sign == 0 ? z : (1 - z)) * (MathF.PI / 2);
    }

    public static float ModByPiP2(float x)
    {
        ScalarMath.Decompose(x, out var sign, out var expo_, out var frac_);
        int start;
        int expo = expo_ - 127 - 23;
        int exShift = 0;
        uint frac = ((uint)frac_) | (1u << 23);
        uint zz;
        while (true)
        {
            start = (expo + 7) / 8;
            zz =
                (+ShiftBoth(frac * ipip2[start + (0 - 1)], expo - (start + 0) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (1 - 1)], expo - (start + 1) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (2 - 1)], expo - (start + 2) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (3 - 1)], expo - (start + 3) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (4 - 1)], expo - (start + 4) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (5 - 1)], expo - (start + 5) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (6 - 1)], expo - (start + 6) * 8 + 32)
            );
            if(zz >= 0x800000)
            {
                break;
            }
            var shift = zz switch
            {
                >= 0x8000 => 8,
                >= 0x80 => 16,
                _ => 24,
            };
            exShift += shift;
            expo += shift;
        }
        var z = zz * MathF.Pow(2, -32 - exShift);
        return (sign == 0 ? z : (1 - z)) * (MathF.PI / 2);
    }

    /// <summary>
    /// <c>x % (PI * 2^n)</c>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static float ModByPiN(float x, int n)
    {
        ScalarMath.Decompose(x, out var sign, out var expo_, out var frac_);
        int start;
        int expo = expo_ - 127 - 23 - (n + 1);
        int exShift = 0;
        uint frac = ((uint)frac_) | (1u << 23);
        uint zz;
        while (true)
        {
            start = (expo + 7) / 8;
            zz =
                (+ShiftBoth(frac * ipip2[start + (0 - 1)], expo - (start + 0) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (1 - 1)], expo - (start + 1) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (2 - 1)], expo - (start + 2) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (3 - 1)], expo - (start + 3) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (4 - 1)], expo - (start + 4) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (5 - 1)], expo - (start + 5) * 8 + 32)
                + ShiftBoth(frac * ipip2[start + (6 - 1)], expo - (start + 6) * 8 + 32)
            );
            if (zz >= 0x800000)
            {
                break;
            }
            var shift = zz switch
            {
                >= 0x8000 => 8,
                >= 0x80 => 16,
                _ => 24,
            };
            exShift += shift;
            expo += shift;
        }
        var z = zz * MathF.Pow(2, -32 - exShift);
        return (sign == 0 ? z : (1 - z)) * (MathF.PI / 2) * MathF.Pow(2, n + 1);
    }
}
