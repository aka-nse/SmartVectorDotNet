using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace SmartVectorDotNet;

public unsafe partial class InternalHelpersTest
{
    [Fact]
    public void CreateVector_Span()
    {
        var elements = Enumerable
            .Range(1, Vector<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector((Span<int>)elements);
        for(int i = 0; i < Vector<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void CreateVector_ROSpan()
    {
        var elements = Enumerable
            .Range(1, Vector<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector((ReadOnlySpan<int>)elements);
        for (int i = 0; i < Vector<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void CreateVector128_Span()
    {
        var elements = Enumerable
            .Range(1, Vector128<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector128((Span<int>)elements);
        for (int i = 0; i < Vector128<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void CreateVector128_ROSpan()
    {
        var elements = Enumerable
            .Range(1, Vector128<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector128((ReadOnlySpan<int>)elements);
        for (int i = 0; i < Vector128<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void CreateVector256_Span()
    {
        var elements = Enumerable
            .Range(1, Vector256<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector256((Span<int>)elements);
        for (int i = 0; i < Vector256<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void CreateVector256_ROSpan()
    {
        var elements = Enumerable
            .Range(1, Vector256<int>.Count)
            .ToArray();
        var vector = InternalHelpers.CreateVector((ReadOnlySpan<int>)elements);
        for (int i = 0; i < Vector256<int>.Count; i++)
        {
            Assert.Equal(elements[i], vector[i]);
        }
    }

    [Fact]
    public void BitCast_Single()
    {
        int x = 42;
        float y = InternalHelpers.BitCast<int, float>(x);
        Assert.Equal(Unsafe.BitCast<int, float>(x), y);
    }

    [Fact]
    public void Reinterpret_Single()
    {
        int x = 42;
        ref float y = ref InternalHelpers.Reinterpret<int, float>(ref x);
        Assert.Equal(Unsafe.BitCast<int, float>(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref y);
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void BitCast_Vector()
    {
        Vector<int> x = new(42);
        Vector<float> y = InternalHelpers.BitCastV<int, float>(x);
        Assert.Equal(Vector.AsVectorSingle(x), y);
    }

    [Fact]
    public void Reinterpret_Vector()
    {
        Vector<int> x = new(42);
        ref Vector<float> y = ref InternalHelpers.ReinterpretV<int, float>(ref x);
        Assert.Equal(Vector.AsVectorSingle(x), y);
        var ptrX = new nint(Unsafe.AsPointer(ref x));
        var ptrY = new nint(Unsafe.AsPointer(ref y));
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void BitCast_Vector256()
    {
        Vector256<int> x = Vector256.Create(42);
        Vector256<float> y = InternalHelpers.BitCast<int, float>(x);
        Assert.Equal(Vector256.AsSingle(x), y);
    }

    [Fact]
    public void BitCast_Vector128()
    {
        Vector128<int> x = Vector128.Create(42);
        Vector128<float> y = InternalHelpers.BitCast<int, float>(x);
        Assert.Equal(Vector128.AsSingle(x), y);
    }

    [Fact]
    public void ReinterpretVArray()
    {
        Vector<int>[] x = [
            .. Enumerable
                .Range(1, 16)
                .Select(i => new Vector<int>(i)),
        ];
        core<int>(x);

        static void core<T>(Vector<int>[] x)
            where T : unmanaged
        {
            Vector<T>[] y = InternalHelpers.ReinterpretVArray<int, T>(x);
            for(var i = 0; i < x.Length; i++)
            {
                Assert.Equal(Unsafe.As<Vector<int>, Vector<T>>(ref x[i]), y[i]);
            }
        }
    }

    [Fact]
    public void AsUnsigned08U()
    {
        Vector<byte> x = new (byte.MaxValue);
        Vector<byte> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned16U()
    {
        Vector<ushort> x = new(ushort.MaxValue);
        Vector<ushort> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned32U()
    {
        Vector<uint> x = new(uint.MaxValue);
        Vector<uint> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned64U()
    {
        Vector<ulong> x = new(ulong.MaxValue);
        Vector<ulong> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsignedNU()
    {
        Vector<nuint> x = new(nuint.MaxValue);
        Vector<nuint> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned08I()
    {
        Vector<sbyte> x = new(sbyte.MaxValue);
        Vector<byte> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal((byte)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned16I()
    {
        Vector<short> x = new(short.MaxValue);
        Vector<ushort> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal((ushort)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned32I()
    {
        Vector<int> x = new(int.MaxValue);
        Vector<uint> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal((uint)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned64I()
    {
        Vector<long> x = new(long.MaxValue);
        Vector<ulong> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal((ulong)x[0], y[0]);
    }

    [Fact]
    public void AsUnsignedNI()
    {
        Vector<nint> x = new(nint.MaxValue);
        Vector<nuint> y = InternalHelpers.AsUnsigned(x);
        Assert.Equal((nuint)x[0], y[0]);
    }

    [Fact]
    public void AsSigned08U()
    {
        Vector<byte> x = new(byte.MaxValue);
        Vector<sbyte> y = InternalHelpers.AsSigned(x);
        Assert.Equal((sbyte)x[0], y[0]);
    }

    [Fact]
    public void AsSigned16U()
    {
        Vector<ushort> x = new(ushort.MaxValue);
        Vector<short> y = InternalHelpers.AsSigned(x);
        Assert.Equal((short)x[0], y[0]);
    }

    [Fact]
    public void AsSigned32U()
    {
        Vector<uint> x = new(uint.MaxValue);
        Vector<int> y = InternalHelpers.AsSigned(x);
        Assert.Equal((int)x[0], y[0]);
    }

    [Fact]
    public void AsSigned64U()
    {
        Vector<ulong> x = new(ulong.MaxValue);
        Vector<long> y = InternalHelpers.AsSigned(x);
        Assert.Equal((long)x[0], y[0]);
    }

    [Fact]
    public void AsSignedNU()
    {
        Vector<nuint> x = new(nuint.MaxValue);
        Vector<nint> y = InternalHelpers.AsSigned(x);
        Assert.Equal((nint)x[0], y[0]);
    }

    [Fact]
    public void AsSigned08I()
    {
        Vector<sbyte> x = new(sbyte.MaxValue);
        Vector<sbyte> y = InternalHelpers.AsSigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned16I()
    {
        Vector<short> x = new(short.MaxValue);
        Vector<short> y = InternalHelpers.AsSigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned32I()
    {
        Vector<int> x = new(int.MaxValue);
        Vector<int> y = InternalHelpers.AsSigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned64I()
    {
        Vector<long> x = new(long.MaxValue);
        Vector<long> y = InternalHelpers.AsSigned(x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSignedNI()
    {
        Vector<nint> x = new(nint.MaxValue);
        Vector<nint> y = InternalHelpers.AsSigned(x);
        Assert.Equal(x[0], y[0]);
    }
}
