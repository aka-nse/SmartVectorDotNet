using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet.PoC;

internal static class ScalarMath_Population
{
    const int Iteration = 1 << 20;

    public static void TestPopulation()
    {
        if (!TestPopulation_Byte())
        {
            Console.WriteLine("Test failed.");
            return;
        }
        if (!TestPopulation_UInt16())
        {
            Console.WriteLine("Test failed.");
            return;
        }
        if (!TestPopulation_UInt32())
        {
            Console.WriteLine("Test failed.");
            return;
        }
        if (!TestPopulation_UInt64())
        {
            Console.WriteLine("Test failed.");
            return;
        }
        Console.WriteLine("Test succeeded.");
    }

    private static bool TestPopulation_Byte()
    {
        var random = new Random(1234567);
        var testValues = Enumerable.Range(0, 256).Select(x => (byte)x);
        foreach (var testValue in testValues)
        {
            if (Population_Reference(testValue) != Population(testValue))
            {
                return false;
            }
        }
        return true;
    }

    private static bool TestPopulation_UInt16()
    {
        var testValues = Enumerable.Range(0, 65536).Select(x => (ushort)x);
        foreach (var testValue in testValues)
        {
            if (Population_Reference(testValue) != Population(testValue))
            {
                return false;
            }
        }
        return true;
    }

    private static bool TestPopulation_UInt32()
    {
        var random = new Random(1234567);
        var testValues = Enumerable.Range(0, Iteration).Select(_ => (uint)random.Next());
        foreach(var testValue in testValues)
        {
            if(Population_Reference(testValue) != Population(testValue))
            {
                return false;
            }
        }
        return true;
    }

    private static bool TestPopulation_UInt64()
    {
        var random = new Random(1234567);
        var testValues = Enumerable.Range(0, Iteration).Select(_ => (ulong)random.NextInt64());
        foreach (var testValue in testValues)
        {
            if (Population_Reference(testValue) != Population(testValue))
            {
                return false;
            }
        }
        return true;
    }

    private static int Population(byte x)
    {
        uint z = x;
        z = (z & 0x55) + ((z >> 1) & 0x55);
        z = (z & 0x33) + ((z >> 2) & 0x33);
        z = (z & 0x0F) + ((z >> 4) & 0x0F);
        return (int)(z & 15);
    }

    private static int Population(ushort x)
    {
        uint z = x;
        z = (z & 0x5555) + ((z >> 1) & 0x5555);
        z = (z & 0x3333) + ((z >> 2) & 0x3333);
        z = (z & 0x0F0F) + ((z >> 4) & 0x0F0F);
        z = (z & 0x00FF) + ((z >> 8) & 0x00FF);
        return (int)(z & 31);
    }

    private static int Population(uint x)
    {
        x = (x & 0x55555555) + ((x >> 1) & 0x55555555);
        x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
        x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);
        x = (x & 0x00FF00FF) + ((x >> 8) & 0x00FF00FF);
        x = (x & 0x0000FFFF) + (x >> 16);
        return (int)(x & 63);
    }

    private static int Population(ulong x)
    {
        x = (x & 0x5555555555555555) + ((x >> 1) & 0x5555555555555555);
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x & 0x0F0F0F0F0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F0F0F0F0F);
        x = (x & 0x00FF00FF00FF00FF) + ((x >> 8) & 0x00FF00FF00FF00FF);
        x = (x & 0x0000FFFF0000FFFF) + ((x >> 16) & 0x0000FFFF0000FFFF);
        x = (x & 0x00000000FFFFFFFF) + (x >> 32);
        return (int)(x & 127);
    }

    private static int Population_Reference(byte x)
    {
        var n = 0;
        for (var i = 0; i < 8; ++i, x >>= 1)
        {
            n += (int)(x & 1);
        }
        return n;
    }

    private static int Population_Reference(ushort x)
    {
        var n = 0;
        for (var i = 0; i < 16; ++i, x >>= 1)
        {
            n += (int)(x & 1);
        }
        return n;
    }

    private static int Population_Reference(uint x)
    {
        var n = 0;
        for(var i = 0; i < 32; ++i, x >>= 1)
        {
            n += (int)(x & 1);
        }
        return n;
    }

    private static int Population_Reference(ulong x)
    {
        var n = 0;
        for (var i = 0; i < 64; ++i, x >>= 1)
        {
            n += (int)(x & 1);
        }
        return n;
    }
}
