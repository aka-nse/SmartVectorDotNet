namespace SmartVectorDotNet;
using H = InternalHelpers;


/// <summary>
/// Provides vectorized mathematical and computational functions.
/// </summary>
public static partial class VectorOp
{
    [AttributeUsage(AttributeTargets.Method)]
    private class VectorOpAttribute : Attribute { }
}