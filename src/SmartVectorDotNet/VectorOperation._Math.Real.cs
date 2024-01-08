using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorOperation
{
}

partial class SimdVectorOperation
{
    [AttributeUsage(AttributeTargets.Method)]
    private class VectorMathAttribute : Attribute { }

    private static class VectorMath
    {
        [VectorMath]
        public static Vector<T> Sqrt<T>(Vector<T> d)
            where T : unmanaged
            => Vector.SquareRoot(d);
    }
}