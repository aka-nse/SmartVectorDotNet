using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

/// <summary>
/// Provides abstraction of vectorized primitive operations.
/// </summary>
public partial class VectorOperation
{
    /// <summary>
    /// Provides <see cref="VectorOperation"/> implementation which uses simple scalar loop logic.
    /// </summary>
    public static VectorOperation Emulated { get; } = new();

    /// <summary>
    /// Provides <see cref="VectorOperation"/> implementation which uses SIMD operators.
    /// </summary>
    public static VectorOperation SIMD { get; } = new SimdVectorOperation();

    /// <summary>  </summary>
    protected VectorOperation() { }
}


/// <summary>
/// The implementation for <see cref="VectorOperation.SIMD"/>.
/// </summary>
public partial class SimdVectorOperation : VectorOperation
{
    internal SimdVectorOperation() { }
}
