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
            => ScalarOp.GreaterThanOrEquals(accuracy, ScalarOp.Const<T>.Zero);

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
            if (mode.HasFlag(AccuracyMode.RelaxNaNCheck))
            {
                var actIsNaN = double.IsNaN(yact) || double.IsInfinity(yact);
                return double.IsNaN(yexp) || (double.IsInfinity(yexp) && actIsNaN);
            }
            else
            {
                var expIsNaN = double.IsNaN(yexp);
                var actIsNaN = double.IsNaN(yact);
                return !(expIsNaN ^ actIsNaN);
            }
        }

        public static partial T ErrorAbs(T exp, T act)
        {
            return ScalarOp.Abs(ScalarOp.Subtract(exp, act));
        }

        public static partial T ErrorRel(T exp, T act)
        {
            var one = ScalarOp.Const<T>.One;
            var maxval = ScalarOp.Const<T>.MaxValue;
            var num = ScalarOp.Abs(ScalarOp.Subtract(exp, act));
            var denom = ScalarOp.Add(ScalarOp.Abs(exp), ScalarOp.Divide(one, maxval));
            return ScalarOp.Divide(num, denom);
        }

        public static partial T ErrorAbsAndRel(T exp, T act)
        {
            return ScalarOp.Max(ErrorAbs(exp, act), ErrorRel(exp, act));
        }

        public static partial T ErrorAbsOrRel(T exp, T act)
        {
            return ScalarOp.Min(ErrorAbs(exp, act), ErrorRel(exp, act));
        }
    }
}
