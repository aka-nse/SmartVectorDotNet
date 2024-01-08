using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

/// <summary>
/// Provides abstraction of vectorized primitive operations.
/// </summary>
public partial class VectorOperation
{
    public static VectorOperation Emulated { get; } = new();

    public static VectorOperation SIMD { get; } = new SimdVectorOperation();

    protected VectorOperation() { }
}


public partial class SimdVectorOperation : VectorOperation
{
    internal SimdVectorOperation() { }
}
