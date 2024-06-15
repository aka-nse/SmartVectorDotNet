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
    public void BitHash32()
    {
        var set = new HashSet<int>();
        for(var i = 0; i < 32; ++i)
        {
            set.Add(ScalarMath.BitHash(1u << i));
        }

        Assert.Equal(32, set.Count);
        for (var i = 0; i < 32; ++i)
        {
            Assert.Contains(i, set);
        }
    }

    [Fact]
    public void BitHash64()
    {
        var set = new HashSet<int>();
        for (var i = 0; i < 64; ++i)
        {
            set.Add(ScalarMath.BitHash(1uL << i));
        }

        Assert.Equal(64, set.Count);
        for (var i = 0; i < 64; ++i)
        {
            Assert.Contains(i, set);
        }
    }
}
