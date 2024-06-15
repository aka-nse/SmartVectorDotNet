using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

namespace SmartVectorDotNet;


file static class TrailingZeroCountTest
{
    public const int Seed = 12345678;
    public const int Iteration = 65536;

    public static void Test<T>()
        where T : unmanaged
    {
        var random = new Random(Seed);
        var buffer = (stackalloc T[Vector<T>.Count]);
        for(var i = 0; i < buffer.Length; ++i)
        {
            random.NextBytes(MemoryMarshal.Cast<T, byte>(buffer));
            var v = InternalHelpers.CreateVector(buffer);
            var z = VectorMath.CountTrailingZeros(v);
            for(var j = 0; j < Vector<T>.Count; ++j)
            {
                var expected = ScalarMath.CountTrailingZeros(v[j]);
                Assert.Equal(expected, z[j]);
            }
        }
    }
}


public partial class VectorMathTest
{
    [Fact] public void CtzUInt8() => TrailingZeroCountTest.Test<byte>();
    [Fact] public void CtzUInt16() => TrailingZeroCountTest.Test<ushort>();
    [Fact] public void CtzUInt32() => TrailingZeroCountTest.Test<uint>();
    [Fact] public void CtzUInt64() => TrailingZeroCountTest.Test<ulong>();
    [Fact] public void CtzUIntN() => TrailingZeroCountTest.Test<nuint>();

    [Fact] public void CtzInt8() => TrailingZeroCountTest.Test<sbyte>();
    [Fact] public void CtzInt16() => TrailingZeroCountTest.Test<short>();
    [Fact] public void CtzInt32() => TrailingZeroCountTest.Test<int>();
    [Fact] public void CtzInt64() => TrailingZeroCountTest.Test<long>();
    [Fact] public void CtzIntN() => TrailingZeroCountTest.Test<nint>();
}
