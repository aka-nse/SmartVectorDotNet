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
    public void Reinterpret_Single()
    {
        int x = 42;
        ref readonly float y = ref InternalHelpers.Reinterpret<int, float>(in x);
        Assert.Equal(Unsafe.BitCast<int, float>(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref Unsafe.AsRef(in y));
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void ReinterpretMutable_Single()
    {
        int x = 42;
        ref float y = ref InternalHelpers.ReinterpretMutable<int, float>(ref x);
        Assert.Equal(Unsafe.BitCast<int, float>(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref y);
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void Reinterpret_Vector()
    {
        Vector<int> x = new(42);
        ref readonly Vector<float> y = ref InternalHelpers.Reinterpret<int, float>(in x);
        Assert.Equal(Vector.AsVectorSingle(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref Unsafe.AsRef(in y));
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void ReinterpretMutable_Vector()
    {
        Vector<int> x = new(42);
        ref Vector<float> y = ref InternalHelpers.ReinterpretMutable<int, float>(ref x);
        Assert.Equal(Vector.AsVectorSingle(x), y);
        var ptrX = new nint(Unsafe.AsPointer(ref x));
        var ptrY = new nint(Unsafe.AsPointer(ref y));
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void Reinterpret_Vector256()
    {
        Vector256<int> x = Vector256.Create(42);
        ref readonly Vector256<float> y = ref InternalHelpers.Reinterpret<int, float>(in x);
        Assert.Equal(Vector256.AsSingle(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref Unsafe.AsRef(in y));
        Assert.Equal(ptrX, ptrY);
    }

    [Fact]
    public void Reinterpret_Vector128()
    {
        Vector128<int> x = Vector128.Create(42);
        ref readonly Vector128<float> y = ref InternalHelpers.Reinterpret<int, float>(in x);
        Assert.Equal(Vector128.AsSingle(x), y);
        var ptrX = (nint)Unsafe.AsPointer(ref x);
        var ptrY = (nint)Unsafe.AsPointer(ref Unsafe.AsRef(in y));
        Assert.Equal(ptrX, ptrY);
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
            ref readonly Vector<T>[] y = ref InternalHelpers.ReinterpretVArray<int, T>(in x);
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
        ref readonly Vector<byte> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned16U()
    {
        Vector<ushort> x = new(ushort.MaxValue);
        ref readonly Vector<ushort> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned32U()
    {
        Vector<uint> x = new(uint.MaxValue);
        ref readonly Vector<uint> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned64U()
    {
        Vector<ulong> x = new(ulong.MaxValue);
        ref readonly Vector<ulong> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsignedNU()
    {
        Vector<nuint> x = new(nuint.MaxValue);
        ref readonly Vector<nuint> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned08I()
    {
        Vector<sbyte> x = new(sbyte.MaxValue);
        ref readonly Vector<byte> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal((byte)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned16I()
    {
        Vector<short> x = new(short.MaxValue);
        ref readonly Vector<ushort> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal((ushort)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned32I()
    {
        Vector<int> x = new(int.MaxValue);
        ref readonly Vector<uint> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal((uint)x[0], y[0]);
    }

    [Fact]
    public void AsUnsigned64I()
    {
        Vector<long> x = new(long.MaxValue);
        ref readonly Vector<ulong> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal((ulong)x[0], y[0]);
    }

    [Fact]
    public void AsUnsignedNI()
    {
        Vector<nint> x = new(nint.MaxValue);
        ref readonly Vector<nuint> y = ref InternalHelpers.AsUnsigned(in x);
        Assert.Equal((nuint)x[0], y[0]);
    }

    [Fact]
    public void AsSigned08U()
    {
        Vector<byte> x = new(byte.MaxValue);
        ref readonly Vector<sbyte> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal((sbyte)x[0], y[0]);
    }

    [Fact]
    public void AsSigned16U()
    {
        Vector<ushort> x = new(ushort.MaxValue);
        ref readonly Vector<short> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal((short)x[0], y[0]);
    }

    [Fact]
    public void AsSigned32U()
    {
        Vector<uint> x = new(uint.MaxValue);
        ref readonly Vector<int> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal((int)x[0], y[0]);
    }

    [Fact]
    public void AsSigned64U()
    {
        Vector<ulong> x = new(ulong.MaxValue);
        ref readonly Vector<long> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal((long)x[0], y[0]);
    }

    [Fact]
    public void AsSignedNU()
    {
        Vector<nuint> x = new(nuint.MaxValue);
        ref readonly Vector<nint> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal((nint)x[0], y[0]);
    }

    [Fact]
    public void AsSigned08I()
    {
        Vector<sbyte> x = new(sbyte.MaxValue);
        ref readonly Vector<sbyte> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned16I()
    {
        Vector<short> x = new(short.MaxValue);
        ref readonly Vector<short> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned32I()
    {
        Vector<int> x = new(int.MaxValue);
        ref readonly Vector<int> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSigned64I()
    {
        Vector<long> x = new(long.MaxValue);
        ref readonly Vector<long> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal(x[0], y[0]);
    }

    [Fact]
    public void AsSignedNI()
    {
        Vector<nint> x = new(nint.MaxValue);
        ref readonly Vector<nint> y = ref InternalHelpers.AsSigned(in x);
        Assert.Equal(x[0], y[0]);
    }
}
