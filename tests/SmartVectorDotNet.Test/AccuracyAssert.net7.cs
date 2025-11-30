using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

partial class AccuracyAssert
{
    partial class Core<T>
    {
        public static partial bool IsValidAccuracy(T accuracy)
            => accuracy >= T.Zero;

        public static partial bool GetIsAccurate(T accuracy, T error)
            => !T.IsNaN(error) && error < accuracy;

        public static partial bool GetIsValidNaN(T zExpected, T zActual, AccuracyMode mode)
        {
            if (mode.HasFlag(AccuracyMode.RelaxNaNCheck))
            {
                var actIsNaN = T.IsNaN(zActual) || T.IsInfinity(zActual);
                return T.IsNaN(zExpected) || (T.IsInfinity(zExpected) && actIsNaN);
            }
            else
            {
                var expIsNaN = T.IsNaN(zExpected);
                var actIsNaN = T.IsNaN(zActual);
                return !(expIsNaN ^ actIsNaN);
            }
        }

        public static partial T ErrorAbs(T exp, T act)
            => T.Abs(exp - act);

        public static partial T ErrorRel(T exp, T act)
            => T.Abs(exp - act) / (T.Abs(exp) + T.One / T.MaxValue);

        public static partial T ErrorAbsAndRel(T exp, T act)
            => T.Max(ErrorAbs(exp, act), ErrorRel(exp, act));

        public static partial T ErrorAbsOrRel(T exp, T act)
            => T.Min(ErrorAbs(exp, act), ErrorRel(exp, act));
    }
}
