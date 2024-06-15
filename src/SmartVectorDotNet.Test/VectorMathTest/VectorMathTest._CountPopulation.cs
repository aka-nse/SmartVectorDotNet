using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    [Fact]
    public void PopulationTest()
    {
        static void core<T>(IEnumerable<T> testValues)
            where T : unmanaged
        {
            var buf = (stackalloc T[Vector<T>.Count]);
            ref readonly var testVector = ref InternalHelpers.Reinterpret<T, Vector<T>>(buf[0]);
            foreach (var testValue in testValues)
            {
                buf[0] = testValue;
                Assert.Equal(
                    ScalarMath.CountPopulation(testValue),
                    VectorMath.CountPopulation(testVector)[0]);
            }
        }

        const int iteration = 1 << 20;
        var random = new Random(1234567);
        core(Enumerable.Range(0, 256).Select(x => (byte)x));
        core(Enumerable.Range(0, 65536).Select(x => (ushort)x));
        core(Enumerable.Range(0, iteration).Select(_ => (uint)random.Next()));
        core(Enumerable.Range(0, iteration).Select(_ => (ulong)random.NextInt64()));
        core(Enumerable.Range(0, 256).Select(x => (sbyte)x));
        core(Enumerable.Range(0, 65536).Select(x => (short)x));
        core(Enumerable.Range(0, iteration).Select(_ => random.Next()));
        core(Enumerable.Range(0, iteration).Select(_ => random.NextInt64()));
    }

}
