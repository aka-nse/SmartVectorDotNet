using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

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
}


/// <summary>
/// The implementation for <see cref="Vectorization.SIMD"/>.
/// </summary>
public partial class SimdVectorization : Vectorization
{
    internal SimdVectorization() { }
}
