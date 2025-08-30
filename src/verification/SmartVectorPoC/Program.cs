// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using SmartVectorDotNet;
using SmartVectorDotNet.PoC;

var buffer = Enumerable.Range(0, 256).Select(x => (byte)x).ToArray();
var vector = GatherUnsafe(
    buffer,
    Vector256.Create([0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07]),
    Vector256.Create([0x28, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F]),
    Vector256.Create([0x40, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17]),
    Vector256.Create([0x88, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F]));
Console.WriteLine($"{vector.ToString("X02")}");

// ScalarOp_SoftwareStrictModulo.TestModulo();
// ScalarOp_Population.TestPopulation();

static unsafe Vector256<byte> GatherUnsafe(ReadOnlySpan<byte> buffer, Vector256<int> index0, Vector256<int> index1, Vector256<int> index2, Vector256<int> index3)
{
    fixed (byte* ptr = buffer)
    {
        var z0 = Avx2.GatherVector256((uint*)ptr, index0, 1);
        var z1 = Avx2.GatherVector256((uint*)ptr, index1, 1);
        var z2 = Avx2.GatherVector256((uint*)ptr, index2, 1);
        var z3 = Avx2.GatherVector256((uint*)ptr, index3, 1);
        return Vector256.Narrow(Vector256.Narrow(z0, z1), Vector256.Narrow(z2, z3));
    }
}