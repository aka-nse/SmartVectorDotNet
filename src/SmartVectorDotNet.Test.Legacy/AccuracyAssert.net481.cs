using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

partial class AccuracyAssert
{
    partial class Core<T>
    {
        public static partial bool IsValidAccuracy(T accuracy)
            => ScalarOp.GreaterThanOrEquals(accuracy, ScalarOp.Zero<T>());

        public static partial bool GetIsAccurate(T accuracy, T error)
        {
            var acc = Convert.ToDouble(accuracy);
            var err = Convert.ToDouble(error);
            return !double.IsNaN(err) && err < acc;
        }

        public static partial bool GetIsValidNaN(T yExpected, T yActual, AccuracyMode mode)
        {
            var yexp = Convert.ToDouble(yExpected);
            var yact = Convert.ToDouble(yActual);
            bool expIsNaN, actIsNaN;
            if (mode.HasFlag(AccuracyMode.RelaxNaNCheck))
            {
                expIsNaN = double.IsNaN(yexp) || double.IsInfinity(yexp);
                actIsNaN = double.IsNaN(yact) || double.IsInfinity(yact);
            }
            else
            {
                expIsNaN = double.IsNaN(yexp);
                actIsNaN = double.IsNaN(yact);
            }
            return !(expIsNaN ^ actIsNaN);
        }

        public static partial T ErrorAbs(T exp, T act)
        {
            return ScalarMath.Abs(ScalarOp.Subtract(exp, act));
        }

        public static partial T ErrorRel(T exp, T act)
        {
            var one = ScalarOp.One<T>();
            var maxval = ScalarOp.MaxValue<T>();
            var num = ScalarMath.Abs(ScalarOp.Subtract(exp, act));
            var denom = ScalarOp.Add(ScalarMath.Abs(exp), ScalarOp.Divide(one, maxval));
            return ScalarOp.Divide(num, denom);
        }

        public static partial T ErrorAbsAndRel(T exp, T act)
        {
            return ScalarMath.Max(ErrorAbs(exp, act), ErrorRel(exp, act));
        }

        public static partial T ErrorAbsOrRel(T exp, T act)
        {
            return ScalarMath.Min(ErrorAbs(exp, act), ErrorRel(exp, act));
        }
    }
}
