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
            { ScalarOp.Const<byte>.MinValue, ScalarOp.Const<byte>.MinValue },
            { ScalarOp.Const<byte>.MinValue, 0 },
            { ScalarOp.Const<byte>.MinValue, ScalarOp.Const<byte>.MaxValue },
            { 0, ScalarOp.Const<byte>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<byte>.MaxValue },
            { ScalarOp.Const<byte>.MaxValue, ScalarOp.Const<byte>.MinValue },
            { ScalarOp.Const<byte>.MaxValue, 0 },
            { ScalarOp.Const<byte>.MaxValue, ScalarOp.Const<byte>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_byte))]
    public void AddSaturate_byte(byte a, byte b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<byte>(a, b);
            var act = VectorOp.AddSaturate<byte>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_byte))]
    public void SubtractSaturate_byte(byte a, byte b)
    {
        var exp = ScalarOp.SubtractSaturate<byte>(a, b);
        var act = VectorOp.SubtractSaturate<byte>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<sbyte, sbyte> SaturateTestCases_sbyte()
        => new (){
            { ScalarOp.Const<sbyte>.MinValue, ScalarOp.Const<sbyte>.MinValue },
            { ScalarOp.Const<sbyte>.MinValue, 0 },
            { ScalarOp.Const<sbyte>.MinValue, ScalarOp.Const<sbyte>.MaxValue },
            { 0, ScalarOp.Const<sbyte>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<sbyte>.MaxValue },
            { ScalarOp.Const<sbyte>.MaxValue, ScalarOp.Const<sbyte>.MinValue },
            { ScalarOp.Const<sbyte>.MaxValue, 0 },
            { ScalarOp.Const<sbyte>.MaxValue, ScalarOp.Const<sbyte>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_sbyte))]
    public void AddSaturate_sbyte(sbyte a, sbyte b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<sbyte>(a, b);
            var act = VectorOp.AddSaturate<sbyte>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_sbyte))]
    public void SubtractSaturate_sbyte(sbyte a, sbyte b)
    {
        var exp = ScalarOp.SubtractSaturate<sbyte>(a, b);
        var act = VectorOp.SubtractSaturate<sbyte>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<short, short> SaturateTestCases_short()
        => new (){
            { ScalarOp.Const<short>.MinValue, ScalarOp.Const<short>.MinValue },
            { ScalarOp.Const<short>.MinValue, 0 },
            { ScalarOp.Const<short>.MinValue, ScalarOp.Const<short>.MaxValue },
            { 0, ScalarOp.Const<short>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<short>.MaxValue },
            { ScalarOp.Const<short>.MaxValue, ScalarOp.Const<short>.MinValue },
            { ScalarOp.Const<short>.MaxValue, 0 },
            { ScalarOp.Const<short>.MaxValue, ScalarOp.Const<short>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_short))]
    public void AddSaturate_short(short a, short b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<short>(a, b);
            var act = VectorOp.AddSaturate<short>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_short))]
    public void SubtractSaturate_short(short a, short b)
    {
        var exp = ScalarOp.SubtractSaturate<short>(a, b);
        var act = VectorOp.SubtractSaturate<short>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<ushort, ushort> SaturateTestCases_ushort()
        => new (){
            { ScalarOp.Const<ushort>.MinValue, ScalarOp.Const<ushort>.MinValue },
            { ScalarOp.Const<ushort>.MinValue, 0 },
            { ScalarOp.Const<ushort>.MinValue, ScalarOp.Const<ushort>.MaxValue },
            { 0, ScalarOp.Const<ushort>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<ushort>.MaxValue },
            { ScalarOp.Const<ushort>.MaxValue, ScalarOp.Const<ushort>.MinValue },
            { ScalarOp.Const<ushort>.MaxValue, 0 },
            { ScalarOp.Const<ushort>.MaxValue, ScalarOp.Const<ushort>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_ushort))]
    public void AddSaturate_ushort(ushort a, ushort b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<ushort>(a, b);
            var act = VectorOp.AddSaturate<ushort>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_ushort))]
    public void SubtractSaturate_ushort(ushort a, ushort b)
    {
        var exp = ScalarOp.SubtractSaturate<ushort>(a, b);
        var act = VectorOp.SubtractSaturate<ushort>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<int, int> SaturateTestCases_int()
        => new (){
            { ScalarOp.Const<int>.MinValue, ScalarOp.Const<int>.MinValue },
            { ScalarOp.Const<int>.MinValue, 0 },
            { ScalarOp.Const<int>.MinValue, ScalarOp.Const<int>.MaxValue },
            { 0, ScalarOp.Const<int>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<int>.MaxValue },
            { ScalarOp.Const<int>.MaxValue, ScalarOp.Const<int>.MinValue },
            { ScalarOp.Const<int>.MaxValue, 0 },
            { ScalarOp.Const<int>.MaxValue, ScalarOp.Const<int>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_int))]
    public void AddSaturate_int(int a, int b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<int>(a, b);
            var act = VectorOp.AddSaturate<int>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_int))]
    public void SubtractSaturate_int(int a, int b)
    {
        var exp = ScalarOp.SubtractSaturate<int>(a, b);
        var act = VectorOp.SubtractSaturate<int>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<uint, uint> SaturateTestCases_uint()
        => new (){
            { ScalarOp.Const<uint>.MinValue, ScalarOp.Const<uint>.MinValue },
            { ScalarOp.Const<uint>.MinValue, 0 },
            { ScalarOp.Const<uint>.MinValue, ScalarOp.Const<uint>.MaxValue },
            { 0, ScalarOp.Const<uint>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<uint>.MaxValue },
            { ScalarOp.Const<uint>.MaxValue, ScalarOp.Const<uint>.MinValue },
            { ScalarOp.Const<uint>.MaxValue, 0 },
            { ScalarOp.Const<uint>.MaxValue, ScalarOp.Const<uint>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_uint))]
    public void AddSaturate_uint(uint a, uint b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<uint>(a, b);
            var act = VectorOp.AddSaturate<uint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_uint))]
    public void SubtractSaturate_uint(uint a, uint b)
    {
        var exp = ScalarOp.SubtractSaturate<uint>(a, b);
        var act = VectorOp.SubtractSaturate<uint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<long, long> SaturateTestCases_long()
        => new (){
            { ScalarOp.Const<long>.MinValue, ScalarOp.Const<long>.MinValue },
            { ScalarOp.Const<long>.MinValue, 0 },
            { ScalarOp.Const<long>.MinValue, ScalarOp.Const<long>.MaxValue },
            { 0, ScalarOp.Const<long>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<long>.MaxValue },
            { ScalarOp.Const<long>.MaxValue, ScalarOp.Const<long>.MinValue },
            { ScalarOp.Const<long>.MaxValue, 0 },
            { ScalarOp.Const<long>.MaxValue, ScalarOp.Const<long>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_long))]
    public void AddSaturate_long(long a, long b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<long>(a, b);
            var act = VectorOp.AddSaturate<long>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_long))]
    public void SubtractSaturate_long(long a, long b)
    {
        var exp = ScalarOp.SubtractSaturate<long>(a, b);
        var act = VectorOp.SubtractSaturate<long>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<ulong, ulong> SaturateTestCases_ulong()
        => new (){
            { ScalarOp.Const<ulong>.MinValue, ScalarOp.Const<ulong>.MinValue },
            { ScalarOp.Const<ulong>.MinValue, 0 },
            { ScalarOp.Const<ulong>.MinValue, ScalarOp.Const<ulong>.MaxValue },
            { 0, ScalarOp.Const<ulong>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<ulong>.MaxValue },
            { ScalarOp.Const<ulong>.MaxValue, ScalarOp.Const<ulong>.MinValue },
            { ScalarOp.Const<ulong>.MaxValue, 0 },
            { ScalarOp.Const<ulong>.MaxValue, ScalarOp.Const<ulong>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_ulong))]
    public void AddSaturate_ulong(ulong a, ulong b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<ulong>(a, b);
            var act = VectorOp.AddSaturate<ulong>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_ulong))]
    public void SubtractSaturate_ulong(ulong a, ulong b)
    {
        var exp = ScalarOp.SubtractSaturate<ulong>(a, b);
        var act = VectorOp.SubtractSaturate<ulong>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<nuint, nuint> SaturateTestCases_nuint()
        => new (){
            { ScalarOp.Const<nuint>.MinValue, ScalarOp.Const<nuint>.MinValue },
            { ScalarOp.Const<nuint>.MinValue, 0 },
            { ScalarOp.Const<nuint>.MinValue, ScalarOp.Const<nuint>.MaxValue },
            { 0, ScalarOp.Const<nuint>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<nuint>.MaxValue },
            { ScalarOp.Const<nuint>.MaxValue, ScalarOp.Const<nuint>.MinValue },
            { ScalarOp.Const<nuint>.MaxValue, 0 },
            { ScalarOp.Const<nuint>.MaxValue, ScalarOp.Const<nuint>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_nuint))]
    public void AddSaturate_nuint(nuint a, nuint b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<nuint>(a, b);
            var act = VectorOp.AddSaturate<nuint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_nuint))]
    public void SubtractSaturate_nuint(nuint a, nuint b)
    {
        var exp = ScalarOp.SubtractSaturate<nuint>(a, b);
        var act = VectorOp.SubtractSaturate<nuint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }


    public static TheoryData<nint, nint> SaturateTestCases_nint()
        => new (){
            { ScalarOp.Const<nint>.MinValue, ScalarOp.Const<nint>.MinValue },
            { ScalarOp.Const<nint>.MinValue, 0 },
            { ScalarOp.Const<nint>.MinValue, ScalarOp.Const<nint>.MaxValue },
            { 0, ScalarOp.Const<nint>.MinValue },
            { 0, 0 },
            { 0, ScalarOp.Const<nint>.MaxValue },
            { ScalarOp.Const<nint>.MaxValue, ScalarOp.Const<nint>.MinValue },
            { ScalarOp.Const<nint>.MaxValue, 0 },
            { ScalarOp.Const<nint>.MaxValue, ScalarOp.Const<nint>.MaxValue },
        };

    [Theory, MemberData(nameof(SaturateTestCases_nint))]
    public void AddSaturate_nint(nint a, nint b)
    {
        unchecked
        {
            var exp = ScalarOp.AddSaturate<nint>(a, b);
            var act = VectorOp.AddSaturate<nint>(new(a), new(b))[0];
            Assert.Equal(exp, act);
        }
    }

    [Theory, MemberData(nameof(SaturateTestCases_nint))]
    public void SubtractSaturate_nint(nint a, nint b)
    {
        var exp = ScalarOp.SubtractSaturate<nint>(a, b);
        var act = VectorOp.SubtractSaturate<nint>(new(a), new(b))[0];
        Assert.Equal(exp, act);
    }

#endif
}
