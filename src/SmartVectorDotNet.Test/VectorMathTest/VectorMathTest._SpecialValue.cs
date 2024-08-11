using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    private sealed class SpecialValueComparer : IEqualityComparer<double>, IEqualityComparer<float>
    {
        public static SpecialValueComparer Instance { get; } = new ();
        private SpecialValueComparer() { }

        public bool Equals(double x, double y)
        {
            if(double.IsNaN(x) && double.IsNaN(y))
            {
                return true;
            }
            if(double.IsNaN(x) && double.IsInfinity(y))
            {
                return true;
            }
            if(double.IsInfinity(x) && double.IsNaN(y))
            {
                return true;
            }
            if(double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y))
            {
                return true;
            }
            if (double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y))
            {
                return true;
            }
            if(x == y)
            {
                return true;
            }
            if(Math.Abs(x - y) < 1e-10)
            {
                return true;
            }
            if(Math.Abs(x - y) / (x * x + y * y) < 1e-10)
            {
                return true;
            }
            return false;
        }

        public bool Equals(float x, float y)
        {
            if (float.IsNaN(x) && float.IsNaN(y))
            {
                return true;
            }
            if (float.IsNaN(x) && float.IsInfinity(y))
            {
                return true;
            }
            if (float.IsInfinity(x) && float.IsNaN(y))
            {
                return true;
            }
            if (float.IsPositiveInfinity(x) && float.IsPositiveInfinity(y))
            {
                return true;
            }
            if (float.IsNegativeInfinity(x) && float.IsNegativeInfinity(y))
            {
                return true;
            }
            if (x == y)
            {
                return true;
            }
            if (MathF.Abs(x - y) < 1e-10f)
            {
                return true;
            }
            if (MathF.Abs(x - y) / (x * x + y * y) < 1e-10f)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] double obj) => 0;
        public int GetHashCode([DisallowNull] float obj) => 0;
    }
}
