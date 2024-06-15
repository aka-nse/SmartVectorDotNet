using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class ScalarMathTest
{
    [Fact]
    public void Ctz32()
    {
        var random = new Random(1234567);
        for(var i = 0; i < 65536; ++i)
        {
            var x = (uint)random.Next();
            Assert.Equal(ctzReference(x), ScalarMath.CountTrailingZeros(x));
        }

        static int ctzReference(uint x)
        {
            var i = 0;
            for(; i < 64; ++i)
            {
                if((x & 1) == 1)
                {
                    return i;
                }
                x >>= 1;
            }
            return i;
        }
    }


    [Fact]
    public void Ctz64()
    {
        var random = new Random(1234567);
        for (var i = 0; i < 65536; ++i)
        {
            var x = (ulong)random.NextInt64();
            Assert.Equal(ctzReference(x), ScalarMath.CountTrailingZeros(x));
        }

        static int ctzReference(ulong x)
        {
            var i = 0;
            for (; i < 64; ++i)
            {
                if ((x & 1) == 1)
                {
                    return i;
                }
                x >>= 1;
            }
            return i;
        }
    }
}
