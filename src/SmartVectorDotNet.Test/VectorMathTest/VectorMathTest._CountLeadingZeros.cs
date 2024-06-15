using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

namespace SmartVectorDotNet;


file static class LeadingZeroCountTest
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
            var z = VectorMath.CountLeadingZeros(v);
            for(var j = 0; j < Vector<T>.Count; ++j)
            {
                var expected = ScalarMath.CountLeadingZeros(v[j]);
                Assert.Equal(expected, z[j]);
            }
        }
    }
}


public partial class VectorMathTest
{
    [Fact] public void ClzUInt8() => LeadingZeroCountTest.Test<byte>();
    [Fact] public void ClzUInt16() => LeadingZeroCountTest.Test<ushort>();
    [Fact] public void ClzUInt32() => LeadingZeroCountTest.Test<uint>();
    [Fact] public void ClzUInt64() => LeadingZeroCountTest.Test<ulong>();
    [Fact] public void ClzUIntN() => LeadingZeroCountTest.Test<nuint>();

    [Fact] public void ClzInt8() => LeadingZeroCountTest.Test<sbyte>();
    [Fact] public void ClzInt16() => LeadingZeroCountTest.Test<short>();
    [Fact] public void ClzInt32() => LeadingZeroCountTest.Test<int>();
    [Fact] public void ClzInt64() => LeadingZeroCountTest.Test<long>();
    [Fact] public void ClzIntN() => LeadingZeroCountTest.Test<nint>();
}
