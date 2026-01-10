using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartVectorDotNet;
using H = InternalHelpers;

/// <summary>
/// Provides abstraction of vectorized primitive operations.
/// </summary>
public partial class Vectorization
{
    /// <summary>
    /// Provides <see cref="Vectorization"/> implementation which uses simple scalar loop logic.
    /// </summary>
    public static Vectorization Emulated { get; } = new();

    /// <summary>
    /// Provides <see cref="Vectorization"/> implementation which uses SIMD operators.
    /// </summary>
    public static Vectorization SIMD { get; } = new SimdVectorization();

    /// <summary>  </summary>
    protected Vectorization() { }

    /// <summary>
    /// Checks for overlap between input and output spans, and allocates a temporary buffer if necessary.
    /// </summary>
    /// <remarks>
    /// This method is set to public to support generators, and it is not recommended to use it directly from user code.
    /// </remarks>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static TemporaryBuffer<T1> EnsureSourceSafe<T1, T2>(ref ReadOnlySpan<T1> source, Span<T2> destination)
        where T1 : unmanaged
        where T2 : unmanaged
    {
        static unsafe bool hasOverlap(ReadOnlySpan<T1> source, Span<T2> destination)
        {
            static ref byte getUntypedRef<T>(in T reference)
                => ref Unsafe.As<T, byte>(ref Unsafe.AsRef(in reference));

            IntPtr s0, s1, d0, d1;
            do
            {
                s0 = Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(source[0]));
                s1 = Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(source[source.Length - 1]));
                d0 = Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(destination[0]));
                d1 = Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(destination[destination.Length - 1]));

                // ensure no memory compaction
                if (Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(source[0])) == s0
                    && Unsafe.ByteOffset(ref getUntypedRef(source[0]), ref getUntypedRef(destination[0])) == d0)
                {
                    break;
                }
            } while (true);

            var ss0 = s0.ToInt64();
            var ss1 = s1.ToInt64();
            var dd0 = d0.ToInt64();
            var dd1 = d1.ToInt64();
            return Math.Max(ss0, dd0) < Math.Min(ss1, dd1);
        }

        if (source.Length == 0 || destination.Length == 0)
        {
            return default;
        }
        if (H.IsSameBuffer(source, destination))
        {
            return default;
        }
        if (!hasOverlap(source, destination))
        {
            return default;
        }
        var retval = new TemporaryBuffer<T1>(source.Length);
        source.CopyTo(retval.Span);
        source = retval.Span;
        return retval;
    }
}


/// <summary>
/// The implementation for <see cref="Vectorization.SIMD"/>.
/// </summary>
public partial class SimdVectorization : Vectorization
{
    /// <summary>
    /// Creates a new instance of <see cref="SimdVectorization"/>.
    /// </summary>
    protected internal SimdVectorization() { }
}
