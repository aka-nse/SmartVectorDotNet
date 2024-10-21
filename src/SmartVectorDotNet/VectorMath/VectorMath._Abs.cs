using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{
    /// <summary> Calculates abs. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Abs<T>(Vector<T> d)
        where T : unmanaged
        => Vector.Abs(d);
}
