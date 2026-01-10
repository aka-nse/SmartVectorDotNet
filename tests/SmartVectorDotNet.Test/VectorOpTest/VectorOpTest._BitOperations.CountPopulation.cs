using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    [Fact]
    public void PopulationTest()
    {
        static void core<T>(IEnumerable<T> testValues)
            where T : unmanaged
        {
            foreach (var testValue in testValues)
            {
                var testVector = new Vector<T>(testValue);
                Assert.Equal(
                    ScalarOp.CountPopulation(testValue),
                    VectorOp.CountPopulation(testVector)[0]);
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
