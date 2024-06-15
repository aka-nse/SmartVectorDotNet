using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


/// <summary>
/// Provides wrapper for "System.Math" which appends genericity and framework compatibility.
/// </summary>
public static partial class ScalarMath
{
    internal static TTo Reinterpret<TFrom, TTo>(in TFrom x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(x));


    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    public partial class Const
    {
        private protected Const() { }
        // 0x8629E8E4B766AFC0uL
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

        /// <summary> Mathematical PI. </summary>
        public static readonly T PI
            = IsT<double>()
                ? Reinterpret(Math.PI)
            : IsT<float>()
                ? Reinterpret((float)Math.PI)
            : default;

        /// <summary> Mathematical E. </summary>
        public static readonly T E
            = IsT<double>()
                ? Reinterpret(Math.E)
            : IsT<float>()
                ? Reinterpret((float)Math.E)
            : default;
    }

}