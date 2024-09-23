﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class ScalarMathTest
{
    [Fact]
    public void Clz32()
    {
        foreach (var x in stackalloc uint[] { 0u, 1u, 2u, 4u, 8u, 16u, 32u, uint.MaxValue, })
        {
            testCore(x);
        }
        var random = new Random(1234567);
        for (var i = 0; i < 65536; ++i)
        {
            var x = (uint)random.Next();
            testCore(x);
        }

        static void testCore(uint x)
            => Assert.Equal(clzReference(x), ScalarMath.CountLeadingZeros(x));

        static int clzReference(uint x)
        {
            var i = 0;
            for (; i < 32; ++i)
            {
                if ((x & 0x8000_0000u) != 0)
                {
                    return i;
                }
                x <<= 1;
            }
            return i;
        }
    }


    [Fact]
    public void Clz64()
    {
        foreach (var x in stackalloc ulong[] { 0u, 1u, 2u, 4u, 8u, 16u, 32u, ulong.MaxValue, })
        {
            testCore(x);
        }
        var random = new Random(1234567);
        for (var i = 0; i < 65536; ++i)
        {
            var hi = (ulong)random.Next();
            var lo = (ulong)random.Next();
            var x = (hi << 32) | lo;
            testCore(x);
        }

        static void testCore(ulong x)
            => Assert.Equal(clzReference(x), ScalarMath.CountLeadingZeros(x));

        static int clzReference(ulong x)
        {
            var i = 0;
            for (; i < 64; ++i)
            {
                if ((x & 0x8000_0000_0000_0000u) != 0)
                {
                    return i;
                }
                x <<= 1;
            }
            return i;
        }
    }
}
