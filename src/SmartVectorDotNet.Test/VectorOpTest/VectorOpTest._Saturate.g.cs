using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
#if NET7_0_OR_GREATER

    public static TheoryData<byte, byte> SaturateTestCases_byte()
        => new (){
            { ScalarOp.MinValue<byte>(), ScalarOp.MinValue<byte>() },
            { ScalarOp.MinValue<byte>(), 0 },
            { ScalarOp.MinValue<byte>(), ScalarOp.MaxValue<byte>() },
            { 0, ScalarOp.MinValue<byte>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<byte>() },
            { ScalarOp.MaxValue<byte>(), ScalarOp.MinValue<byte>() },
            { ScalarOp.MaxValue<byte>(), 0 },
            { ScalarOp.MaxValue<byte>(), ScalarOp.MaxValue<byte>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_byte))]
    public void AddSaturate_byte(byte a, byte b)
    {
        unchecked
        {
            var exp = (byte)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<byte>(), ScalarOp.MaxValue<byte>());
            var act = VectorOp.AddSaturate<byte>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_byte))]
    public void SubtractSaturate_byte(byte a, byte b)
    {
        var exp = (byte)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<byte>(), ScalarOp.MaxValue<byte>());
        var act = VectorOp.SubtractSaturate<byte>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<sbyte, sbyte> SaturateTestCases_sbyte()
        => new (){
            { ScalarOp.MinValue<sbyte>(), ScalarOp.MinValue<sbyte>() },
            { ScalarOp.MinValue<sbyte>(), 0 },
            { ScalarOp.MinValue<sbyte>(), ScalarOp.MaxValue<sbyte>() },
            { 0, ScalarOp.MinValue<sbyte>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<sbyte>() },
            { ScalarOp.MaxValue<sbyte>(), ScalarOp.MinValue<sbyte>() },
            { ScalarOp.MaxValue<sbyte>(), 0 },
            { ScalarOp.MaxValue<sbyte>(), ScalarOp.MaxValue<sbyte>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_sbyte))]
    public void AddSaturate_sbyte(sbyte a, sbyte b)
    {
        unchecked
        {
            var exp = (sbyte)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<sbyte>(), ScalarOp.MaxValue<sbyte>());
            var act = VectorOp.AddSaturate<sbyte>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_sbyte))]
    public void SubtractSaturate_sbyte(sbyte a, sbyte b)
    {
        var exp = (sbyte)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<sbyte>(), ScalarOp.MaxValue<sbyte>());
        var act = VectorOp.SubtractSaturate<sbyte>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<short, short> SaturateTestCases_short()
        => new (){
            { ScalarOp.MinValue<short>(), ScalarOp.MinValue<short>() },
            { ScalarOp.MinValue<short>(), 0 },
            { ScalarOp.MinValue<short>(), ScalarOp.MaxValue<short>() },
            { 0, ScalarOp.MinValue<short>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<short>() },
            { ScalarOp.MaxValue<short>(), ScalarOp.MinValue<short>() },
            { ScalarOp.MaxValue<short>(), 0 },
            { ScalarOp.MaxValue<short>(), ScalarOp.MaxValue<short>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_short))]
    public void AddSaturate_short(short a, short b)
    {
        unchecked
        {
            var exp = (short)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<short>(), ScalarOp.MaxValue<short>());
            var act = VectorOp.AddSaturate<short>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_short))]
    public void SubtractSaturate_short(short a, short b)
    {
        var exp = (short)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<short>(), ScalarOp.MaxValue<short>());
        var act = VectorOp.SubtractSaturate<short>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<ushort, ushort> SaturateTestCases_ushort()
        => new (){
            { ScalarOp.MinValue<ushort>(), ScalarOp.MinValue<ushort>() },
            { ScalarOp.MinValue<ushort>(), 0 },
            { ScalarOp.MinValue<ushort>(), ScalarOp.MaxValue<ushort>() },
            { 0, ScalarOp.MinValue<ushort>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<ushort>() },
            { ScalarOp.MaxValue<ushort>(), ScalarOp.MinValue<ushort>() },
            { ScalarOp.MaxValue<ushort>(), 0 },
            { ScalarOp.MaxValue<ushort>(), ScalarOp.MaxValue<ushort>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_ushort))]
    public void AddSaturate_ushort(ushort a, ushort b)
    {
        unchecked
        {
            var exp = (ushort)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<ushort>(), ScalarOp.MaxValue<ushort>());
            var act = VectorOp.AddSaturate<ushort>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_ushort))]
    public void SubtractSaturate_ushort(ushort a, ushort b)
    {
        var exp = (ushort)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<ushort>(), ScalarOp.MaxValue<ushort>());
        var act = VectorOp.SubtractSaturate<ushort>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<int, int> SaturateTestCases_int()
        => new (){
            { ScalarOp.MinValue<int>(), ScalarOp.MinValue<int>() },
            { ScalarOp.MinValue<int>(), 0 },
            { ScalarOp.MinValue<int>(), ScalarOp.MaxValue<int>() },
            { 0, ScalarOp.MinValue<int>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<int>() },
            { ScalarOp.MaxValue<int>(), ScalarOp.MinValue<int>() },
            { ScalarOp.MaxValue<int>(), 0 },
            { ScalarOp.MaxValue<int>(), ScalarOp.MaxValue<int>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_int))]
    public void AddSaturate_int(int a, int b)
    {
        unchecked
        {
            var exp = (int)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<int>(), ScalarOp.MaxValue<int>());
            var act = VectorOp.AddSaturate<int>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_int))]
    public void SubtractSaturate_int(int a, int b)
    {
        var exp = (int)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<int>(), ScalarOp.MaxValue<int>());
        var act = VectorOp.SubtractSaturate<int>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<uint, uint> SaturateTestCases_uint()
        => new (){
            { ScalarOp.MinValue<uint>(), ScalarOp.MinValue<uint>() },
            { ScalarOp.MinValue<uint>(), 0 },
            { ScalarOp.MinValue<uint>(), ScalarOp.MaxValue<uint>() },
            { 0, ScalarOp.MinValue<uint>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<uint>() },
            { ScalarOp.MaxValue<uint>(), ScalarOp.MinValue<uint>() },
            { ScalarOp.MaxValue<uint>(), 0 },
            { ScalarOp.MaxValue<uint>(), ScalarOp.MaxValue<uint>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_uint))]
    public void AddSaturate_uint(uint a, uint b)
    {
        unchecked
        {
            var exp = (uint)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<uint>(), ScalarOp.MaxValue<uint>());
            var act = VectorOp.AddSaturate<uint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_uint))]
    public void SubtractSaturate_uint(uint a, uint b)
    {
        var exp = (uint)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<uint>(), ScalarOp.MaxValue<uint>());
        var act = VectorOp.SubtractSaturate<uint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<long, long> SaturateTestCases_long()
        => new (){
            { ScalarOp.MinValue<long>(), ScalarOp.MinValue<long>() },
            { ScalarOp.MinValue<long>(), 0 },
            { ScalarOp.MinValue<long>(), ScalarOp.MaxValue<long>() },
            { 0, ScalarOp.MinValue<long>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<long>() },
            { ScalarOp.MaxValue<long>(), ScalarOp.MinValue<long>() },
            { ScalarOp.MaxValue<long>(), 0 },
            { ScalarOp.MaxValue<long>(), ScalarOp.MaxValue<long>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_long))]
    public void AddSaturate_long(long a, long b)
    {
        unchecked
        {
            var exp = (long)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<long>(), ScalarOp.MaxValue<long>());
            var act = VectorOp.AddSaturate<long>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_long))]
    public void SubtractSaturate_long(long a, long b)
    {
        var exp = (long)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<long>(), ScalarOp.MaxValue<long>());
        var act = VectorOp.SubtractSaturate<long>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<ulong, ulong> SaturateTestCases_ulong()
        => new (){
            { ScalarOp.MinValue<ulong>(), ScalarOp.MinValue<ulong>() },
            { ScalarOp.MinValue<ulong>(), 0 },
            { ScalarOp.MinValue<ulong>(), ScalarOp.MaxValue<ulong>() },
            { 0, ScalarOp.MinValue<ulong>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<ulong>() },
            { ScalarOp.MaxValue<ulong>(), ScalarOp.MinValue<ulong>() },
            { ScalarOp.MaxValue<ulong>(), 0 },
            { ScalarOp.MaxValue<ulong>(), ScalarOp.MaxValue<ulong>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_ulong))]
    public void AddSaturate_ulong(ulong a, ulong b)
    {
        unchecked
        {
            var exp = (ulong)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<ulong>(), ScalarOp.MaxValue<ulong>());
            var act = VectorOp.AddSaturate<ulong>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_ulong))]
    public void SubtractSaturate_ulong(ulong a, ulong b)
    {
        var exp = (ulong)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<ulong>(), ScalarOp.MaxValue<ulong>());
        var act = VectorOp.SubtractSaturate<ulong>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<nuint, nuint> SaturateTestCases_nuint()
        => new (){
            { ScalarOp.MinValue<nuint>(), ScalarOp.MinValue<nuint>() },
            { ScalarOp.MinValue<nuint>(), 0 },
            { ScalarOp.MinValue<nuint>(), ScalarOp.MaxValue<nuint>() },
            { 0, ScalarOp.MinValue<nuint>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<nuint>() },
            { ScalarOp.MaxValue<nuint>(), ScalarOp.MinValue<nuint>() },
            { ScalarOp.MaxValue<nuint>(), 0 },
            { ScalarOp.MaxValue<nuint>(), ScalarOp.MaxValue<nuint>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_nuint))]
    public void AddSaturate_nuint(nuint a, nuint b)
    {
        unchecked
        {
            var exp = (nuint)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<nuint>(), ScalarOp.MaxValue<nuint>());
            var act = VectorOp.AddSaturate<nuint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_nuint))]
    public void SubtractSaturate_nuint(nuint a, nuint b)
    {
        var exp = (nuint)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<nuint>(), ScalarOp.MaxValue<nuint>());
        var act = VectorOp.SubtractSaturate<nuint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<nint, nint> SaturateTestCases_nint()
        => new (){
            { ScalarOp.MinValue<nint>(), ScalarOp.MinValue<nint>() },
            { ScalarOp.MinValue<nint>(), 0 },
            { ScalarOp.MinValue<nint>(), ScalarOp.MaxValue<nint>() },
            { 0, ScalarOp.MinValue<nint>() },
            { 0, 0 },
            { 0, ScalarOp.MaxValue<nint>() },
            { ScalarOp.MaxValue<nint>(), ScalarOp.MinValue<nint>() },
            { ScalarOp.MaxValue<nint>(), 0 },
            { ScalarOp.MaxValue<nint>(), ScalarOp.MaxValue<nint>() },
        };

    [Theory, MemberData(nameof(SaturateTestCases_nint))]
    public void AddSaturate_nint(nint a, nint b)
    {
        unchecked
        {
            var exp = (nint)Int128.Clamp((Int128)a + b, ScalarOp.MinValue<nint>(), ScalarOp.MaxValue<nint>());
            var act = VectorOp.AddSaturate<nint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_nint))]
    public void SubtractSaturate_nint(nint a, nint b)
    {
        var exp = (nint)Int128.Clamp((Int128)a - b, ScalarOp.MinValue<nint>(), ScalarOp.MaxValue<nint>());
        var act = VectorOp.SubtractSaturate<nint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }

#endif
}
