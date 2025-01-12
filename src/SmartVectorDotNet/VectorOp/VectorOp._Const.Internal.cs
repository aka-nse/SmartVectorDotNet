using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorOp
{
    partial class Const<T>
    {
        internal static readonly Vector<T> PI_1p2 = AsVector(0.5) * PI;
        internal static readonly Vector<T> PI_2p2 = AsVector(1.0) * PI;
        internal static readonly Vector<T> PI_3p2 = AsVector(1.5) * PI;
        internal static readonly Vector<T> PI_4p2 = AsVector(2.0) * PI;


        internal static readonly Vector<T> _m1
            = AsVector(-1.0);

        internal static readonly Vector<T> _0
            = AsVector(0.0);

        internal static readonly Vector<T> _1p2
            = AsVector(0.5);

        internal static readonly Vector<T> _2p3
            = AsVector(2.0 / 3.0);

        internal static readonly Vector<T> _1
            = AsVector(1.0);

        internal static readonly Vector<T> _Sqrt2
            = AsVector(ScalarOp.Sqrt(2.0));

        internal static readonly Vector<T> _2
            = AsVector(2.0);

        internal static readonly Vector<T> _3
            = AsVector(3.0);

        internal static readonly Vector<T> Log_2_E = AsVector(ScalarOp.Log(ScalarOp.Const<double>.E, 2));
        internal static readonly Vector<T> Log_E_2 = AsVector(ScalarOp.Log(2, ScalarOp.Const<double>.E));
    }
}
