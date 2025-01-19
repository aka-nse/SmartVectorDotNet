using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates abs. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Abs<T>(Vector<T> d)
        where T : unmanaged
        => Vector.Abs(d);
}
