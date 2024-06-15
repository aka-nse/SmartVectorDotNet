﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY T4. DO NOT CHANGE IT. CHANGE THE .tt FILE INSTEAD.
// </auto-generated>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif
using System.Text;
using NVector = System.Numerics.Vector;
using H = SmartVectorDotNet.InternalHelpers;

using Xunit;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{

    public static IEnumerable<object[]> ByteTestCases()
    {
        static object[] core(byte[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(byte)0}.Concat(Enumerable.Repeat((byte)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(ByteTestCases))]
    public void ShiftLeft_Byte(byte[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<byte>(value);
            var vshiftCount = new Vector<byte>((byte)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<byte>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<byte>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(ByteTestCases))]
    public void ShiftRightLogical_Byte(byte[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<byte>(value);
            var vshiftCount = new Vector<byte>((byte)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<byte>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<byte>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }



    public static IEnumerable<object[]> UInt16TestCases()
    {
        static object[] core(ushort[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(ushort)0}.Concat(Enumerable.Repeat((ushort)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(UInt16TestCases))]
    public void ShiftLeft_UInt16(ushort[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<ushort>(value);
            var vshiftCount = new Vector<ushort>((ushort)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<ushort>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<ushort>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(UInt16TestCases))]
    public void ShiftRightLogical_UInt16(ushort[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<ushort>(value);
            var vshiftCount = new Vector<ushort>((ushort)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<ushort>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<ushort>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }



    public static IEnumerable<object[]> UInt32TestCases()
    {
        static object[] core(uint[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(uint)0}.Concat(Enumerable.Repeat((uint)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(UInt32TestCases))]
    public void ShiftLeft_UInt32(uint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<uint>(value);
            var vshiftCount = new Vector<uint>((uint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<uint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<uint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(UInt32TestCases))]
    public void ShiftRightLogical_UInt32(uint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<uint>(value);
            var vshiftCount = new Vector<uint>((uint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<uint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<uint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }



    public static IEnumerable<object[]> UInt64TestCases()
    {
        static object[] core(ulong[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(ulong)0}.Concat(Enumerable.Repeat((ulong)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(UInt64TestCases))]
    public void ShiftLeft_UInt64(ulong[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<ulong>(value);
            var vshiftCount = new Vector<ulong>((ulong)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<ulong>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<ulong>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(UInt64TestCases))]
    public void ShiftRightLogical_UInt64(ulong[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<ulong>(value);
            var vshiftCount = new Vector<ulong>((ulong)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<ulong>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<ulong>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }



    public static IEnumerable<object[]> UIntPtrTestCases()
    {
        static object[] core(nuint[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(nuint)0}.Concat(Enumerable.Repeat((nuint)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(UIntPtrTestCases))]
    public void ShiftLeft_UIntPtr(nuint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<nuint>(value);
            var vshiftCount = new Vector<nuint>((nuint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<nuint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<nuint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(UIntPtrTestCases))]
    public void ShiftRightLogical_UIntPtr(nuint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<nuint>(value);
            var vshiftCount = new Vector<nuint>((nuint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<nuint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<nuint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }



    public static IEnumerable<object[]> SByteTestCases()
    {
        static object[] core(sbyte[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(sbyte)0}.Concat(Enumerable.Repeat((sbyte)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(SByteTestCases))]
    public void ShiftLeft_SByte(sbyte[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<sbyte>(value);
            var vshiftCount = new Vector<sbyte>((sbyte)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<sbyte>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<sbyte>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(SByteTestCases))]
    public void ShiftRightLogical_SByte(sbyte[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<sbyte>(value);
            var vshiftCount = new Vector<sbyte>((sbyte)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<sbyte>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<sbyte>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(SByteTestCases))]
    public void ShiftRightArithmetic_SByte(sbyte[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<sbyte>(value);
            var vshiftCount = new Vector<sbyte>((sbyte)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<sbyte>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<sbyte>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }


    public static IEnumerable<object[]> Int16TestCases()
    {
        static object[] core(short[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(short)0}.Concat(Enumerable.Repeat((short)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(Int16TestCases))]
    public void ShiftLeft_Int16(short[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<short>(value);
            var vshiftCount = new Vector<short>((short)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<short>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<short>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int16TestCases))]
    public void ShiftRightLogical_Int16(short[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<short>(value);
            var vshiftCount = new Vector<short>((short)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<short>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<short>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int16TestCases))]
    public void ShiftRightArithmetic_Int16(short[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<short>(value);
            var vshiftCount = new Vector<short>((short)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<short>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<short>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }


    public static IEnumerable<object[]> Int32TestCases()
    {
        static object[] core(int[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(int)0}.Concat(Enumerable.Repeat((int)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(Int32TestCases))]
    public void ShiftLeft_Int32(int[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<int>(value);
            var vshiftCount = new Vector<int>((int)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<int>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<int>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int32TestCases))]
    public void ShiftRightLogical_Int32(int[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<int>(value);
            var vshiftCount = new Vector<int>((int)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<int>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<int>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int32TestCases))]
    public void ShiftRightArithmetic_Int32(int[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<int>(value);
            var vshiftCount = new Vector<int>((int)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<int>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<int>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }


    public static IEnumerable<object[]> Int64TestCases()
    {
        static object[] core(long[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(long)0}.Concat(Enumerable.Repeat((long)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(Int64TestCases))]
    public void ShiftLeft_Int64(long[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<long>(value);
            var vshiftCount = new Vector<long>((long)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<long>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<long>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int64TestCases))]
    public void ShiftRightLogical_Int64(long[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<long>(value);
            var vshiftCount = new Vector<long>((long)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<long>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<long>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(Int64TestCases))]
    public void ShiftRightArithmetic_Int64(long[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<long>(value);
            var vshiftCount = new Vector<long>((long)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<long>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<long>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }


    public static IEnumerable<object[]> IntPtrTestCases()
    {
        static object[] core(nint[] valueArray, byte[] shiftCountArray)
            => new object[] { valueArray, shiftCountArray };

        var valueArray = new [] {(nint)0}.Concat(Enumerable.Repeat((nint)1, 256)).ToArray();
        var shiftCountArray = new [] {(byte)0}.Concat(Enumerable.Range(0, 256).Select(x => (byte)x)).ToArray();
        yield return core(valueArray, shiftCountArray);
    }

    [Theory, MemberData(nameof(IntPtrTestCases))]
    public void ShiftLeft_IntPtr(nint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<nint>(value);
            var vshiftCount = new Vector<nint>((nint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, shiftCount )[0], VectorOp.ShiftLeft<nint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftLeftFallback(vvalue, vshiftCount)[0], VectorOp.ShiftLeft<nint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(IntPtrTestCases))]
    public void ShiftRightLogical_IntPtr(nint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<nint>(value);
            var vshiftCount = new Vector<nint>((nint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<nint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightLogicalFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<nint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

    [Theory, MemberData(nameof(IntPtrTestCases))]
    public void ShiftRightArithmetic_IntPtr(nint[] valueArray, byte[] shiftCountArray)
    {
        foreach(var (value, shiftCount) in valueArray.Zip(shiftCountArray))
        {
            var vvalue = new Vector<nint>(value);
            var vshiftCount = new Vector<nint>((nint)shiftCount);
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, shiftCount )[0], VectorOp.ShiftRightLogical<nint>(vvalue, shiftCount )[0], $"shiftCount = {shiftCount}");
            AssertEx.Equal(VectorOp.ShiftRightArithmeticFallback(vvalue, vshiftCount)[0], VectorOp.ShiftRightLogical<nint>(vvalue, vshiftCount)[0], $"shiftCount = {shiftCount}");
        }
    }

}

