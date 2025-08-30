using System;
using System.Collections.Generic;
using System.Text;
using GenericSpecialization;

namespace SmartVectorDotNet;

partial class ScalarOp
{
    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    public partial class Const
    {
        private protected Const() { }

        [PrimaryGeneric(nameof(GetConst_default))]
        private protected static partial void GetConst<T>(out T zero, out T one, out T minValue, out T maxValue, out T msb, out T lsb);
        private static void GetConst_default<T>(out T zero, out T one, out T minValue, out T maxValue, out T msb, out T lsb)
        {
            zero = default!;
            one = default!;
            minValue = default!;
            maxValue = default!;
            msb = default!;
            lsb = default!;
        }
        private static void GetConst(out byte zero, out byte one, out byte minValue, out byte maxValue, out byte msb, out byte lsb)
        {
            zero = 0;
            one = 1;
            minValue = byte.MinValue;
            maxValue = byte.MaxValue;
            msb = 0x80;
            lsb = 1;
        }
        private static void GetConst(out ushort zero, out ushort one, out ushort minValue, out ushort maxValue, out ushort msb, out ushort lsb)
        {
            zero = 0;
            one = 1;
            minValue = ushort.MinValue;
            maxValue = ushort.MaxValue;
            msb = 0x8000;
            lsb = 1;
        }
        private static void GetConst(out uint zero, out uint one, out uint minValue, out uint maxValue, out uint msb, out uint lsb)
        {
            zero = 0;
            one = 1;
            minValue = uint.MinValue;
            maxValue = uint.MaxValue;
            msb = 0x8000_0000;
            lsb = 1;
        }
        private static void GetConst(out ulong zero, out ulong one, out ulong minValue, out ulong maxValue, out ulong msb, out ulong lsb)
        {
            zero = 0;
            one = 1;
            minValue = ulong.MinValue;
            maxValue = ulong.MaxValue;
            msb = 0x8000_0000_0000_0000;
            lsb = 1;
        }
        private static void GetConst(out sbyte zero, out sbyte one, out sbyte minValue, out sbyte maxValue, out sbyte msb, out sbyte lsb)
        {
            zero = 0;
            one = 1;
            minValue = sbyte.MinValue;
            maxValue = sbyte.MaxValue;
            msb = unchecked((sbyte)0x80);
            lsb = 1;
        }
        private static void GetConst(out short zero, out short one, out short minValue, out short maxValue, out short msb, out short lsb)
        {
            zero = 0;
            one = 1;
            minValue = short.MinValue;
            maxValue = short.MaxValue;
            msb = unchecked((short)0x8000);
            lsb = 1;
        }
        private static void GetConst(out int zero, out int one, out int minValue, out int maxValue, out int msb, out int lsb)
        {
            zero = 0;
            one = 1;
            minValue = int.MinValue;
            maxValue = int.MaxValue;
            msb = unchecked((int)0x8000_0000);
            lsb = 1;
        }
        private static void GetConst(out long zero, out long one, out long minValue, out long maxValue, out long msb, out long lsb)
        {
            zero = 0;
            one = 1;
            minValue = long.MinValue;
            maxValue = long.MaxValue;
            msb = unchecked((long)0x8000_0000_0000_0000);
            lsb = 1;
        }
        private static void GetConst(out nuint zero, out nuint one, out nuint minValue, out nuint maxValue, out nuint msb, out nuint lsb)
        {
            unchecked
            {
                zero = 0;
                one = 1;
                (minValue, maxValue, msb) = IntPtr.Size == 8
                    ? ((nuint)ulong.MinValue, (nuint)ulong.MaxValue, (nuint)0x8000_0000_0000_0000)
                    : (uint.MinValue, uint.MaxValue, 0x8000_0000);
                lsb = 1;
            }
        }
        private static void GetConst(out nint zero, out nint one, out nint minValue, out nint maxValue, out nint msb, out nint lsb)
        {
            unchecked
            {
                zero = 0;
                one = 1;
                (minValue, maxValue, msb) = IntPtr.Size == 8
                    ? ((nint)long.MinValue, (nint)long.MaxValue, (nint)0x8000_0000_0000_0000)
                    : (int.MinValue, int.MaxValue, (nint)0x8000_0000);
                lsb = 1;
            }
        }
        private static void GetConst(out float zero, out float one, out float minValue, out float maxValue, out float msb, out float lsb)
        {
            zero = 0;
            one = 1;
            minValue = float.MinValue;
            maxValue = float.MaxValue;
            msb = Reinterpret<uint, float>(0x8000_0000);
            lsb = Reinterpret<uint, float>(1);
        }
        private static void GetConst(out double zero, out double one, out double minValue, out double maxValue, out double msb, out double lsb)
        {
            zero = 0;
            one = 1;
            minValue = double.MinValue;
            maxValue = double.MaxValue;
            msb = Reinterpret<ulong, double>(0x8000_0000_0000_0000);
            lsb = Reinterpret<ulong, double>(1);
        }

        [PrimaryGeneric(nameof(GetPI_default))]
        private protected static partial T GetPI<T>(T _);
        private static T GetPI_default<T>(T _) => default!;
        private static float GetPI(float _) => (float)Math.PI;
        private static double GetPI(double _) => Math.PI;

        [PrimaryGeneric(nameof(GetE_default))]
        private protected static partial T GetE<T>(T _);
        private static T GetE_default<T>(T _) => default!;
        private static float GetE(float _) => (float)Math.E;
        private static double GetE(double _) => Math.E;

        [PrimaryGeneric(nameof(GetNaN_default))]
        private protected static partial T GetNaN<T>(T _);
        private static T GetNaN_default<T>(T _) => default!;
        private static float GetNaN(float _) => float.NaN;
        private static double GetNaN(double _) => double.NaN;

        [PrimaryGeneric(nameof(GetPInf_default))]
        private protected static partial T GetPInf<T>(T _);
        private static T GetPInf_default<T>(T _) => default!;
        private static float GetPInf(float _) => float.PositiveInfinity;
        private static double GetPInf(double _) => double.PositiveInfinity;

        [PrimaryGeneric(nameof(GetNInf_default))]
        private protected static partial T GetNInf<T>(T _);
        private static T GetNInf_default<T>(T _) => default!;
        private static float GetNInf(float _) => float.NegativeInfinity;
        private static double GetNInf(double _) => double.NegativeInfinity;
    }


    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Const<T> : Const
        where T : unmanaged
    {
        private static bool IsT<TEntity>()
            => typeof(T) == typeof(TEntity);

        private static T Reinterpret<TFrom>(TFrom x)
            where TFrom : unmanaged
            => Reinterpret<TFrom, T>(x);

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to <c>0</c>. </summary>
        public static readonly T Zero;

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to <c>1</c>. </summary>
        public static readonly T One;

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to minimum value. </summary>
        public static readonly T MinValue;

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to maximum value. </summary>
        public static readonly T MaxValue;

        /// <summary> Gets a value whose MSB is 1. </summary>
        public static readonly T Msb;

        /// <summary> Gets a value whose LSB is 1. </summary>
        public static readonly T Lsb;

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to <c>true</c> on SIMD operation. </summary>
        public static readonly T TrueValue;

        /// <summary> Gets a value of <typeparamref name="T"/> which is corresponding to <c>false</c> on SIMD operation. </summary>
        public static readonly T FalseValue;

        /// <summary> Mathematical PI. </summary>
        public static readonly T PI = GetPI(default(T));

        /// <summary> Mathematical E. </summary>
        public static readonly T E = GetE(default(T));

        /// <summary> NaN value. </summary>
        public static readonly T NaN = GetNaN(default(T));

        /// <summary> Positive infinity. </summary>
        public static readonly T PInf = GetPInf(default(T));

        /// <summary> Negative infinity. </summary>
        public static readonly T NInf = GetNInf(default(T));

        static Const()
        {
            GetConst(out Zero, out One, out MinValue, out MaxValue, out Msb, out Lsb);
            TrueValue = Vector.Equals(Vector<T>.Zero, Vector<T>.Zero)[0];
            FalseValue = Vector.Equals(Vector<T>.Zero, Vector<T>.One)[0];
        }

        private protected Const() { }
    }
}
