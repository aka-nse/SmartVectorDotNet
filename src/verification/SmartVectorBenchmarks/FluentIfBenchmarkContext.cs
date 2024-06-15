using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Numerics.Vector;

namespace SmartVectorBenchmarks;

public class FluentIfBenchmarkContext
{
    private static readonly Vector<int>[] X = Enumerable.Range(0, 1 << 16).Select(i => new Vector<int>(i)).ToArray();


    public FluentIfBenchmarkContext()
    {
        var a = Simple();
        var b = Simple();
        if (!a.SequenceEqual(b))
        {
            throw new InvalidOperationException();
        }
    }


    [Benchmark]
    public Vector<int>[] Simple()
    {
        var retval = new Vector<int>[X.Length];
        for (var i = 0; i < X.Length; ++i)
        {
            retval[i] = Vector.ConditionalSelect(
                CanDivideBy7(X[i]),
                _7,
                Vector.ConditionalSelect(
                    CanDivideBy5(X[i]),
                    -_5,
                    Vector.ConditionalSelect(
                        CanDivideBy3(X[i]),
                        -_3,
                        Vector.ConditionalSelect(
                            CanDivideBy2(X[i]),
                            -_2,
                            -Vector<int>.One
                            )
                        )
                    )
                );
        }
        return retval;
    }


    [Benchmark]
    public Vector<int>[] Fluent()
    {
        var retval = new Vector<int>[X.Length];
        for (var i = 0; i < X.Length; ++i)
        {
            retval[i] = VectorOp
                .If(CanDivideBy7(X[i]), _7)
                .Elif(CanDivideBy5(X[i]), -_5)
                .Elif(CanDivideBy3(X[i]), -_3)
                .Elif(CanDivideBy2(X[i]), -_2)
                .Else(-Vector<int>.One);
        }
        return retval;
    }


    private static readonly Vector<int> _2 = new(2);
    private static Vector<int> CanDivideBy2(in Vector<int> x)
        => Vector.Equals(Vector<int>.Zero, x - _2 * (x / _2));

    private static readonly Vector<int> _3 = new(3);
    private static Vector<int> CanDivideBy3(in Vector<int> x)
        => Vector.Equals(Vector<int>.Zero, x - _3 * (x / _3));

    private static readonly Vector<int> _5 = new(5);
    private static Vector<int> CanDivideBy5(in Vector<int> x)
        => Vector.Equals(Vector<int>.Zero, x - _5 * (x / _5));

    private static readonly Vector<int> _7 = new(7);
    private static Vector<int> CanDivideBy7(in Vector<int> x)
        => Vector.Equals(Vector<int>.Zero, x - _7 * (x / _7));

}

public static class VectorOp
{
    public readonly ref struct ConditionalSelector<TFlag, TValue>
        where TFlag : unmanaged
        where TValue : unmanaged
    {
        private readonly Vector<TFlag> _Flags;
        private readonly Vector<TValue> _Value;

        internal ConditionalSelector(in Vector<TFlag> flags, in Vector<TValue> value)
        {
            _Flags = flags;
            _Value = BitwiseAnd(As<TFlag, TValue>(flags), value);
        }

        internal ConditionalSelector(in ConditionalSelector<TFlag, TValue> forwards, in Vector<TFlag> condition, in Vector<TValue> then)
        {
            _Flags = BitwiseOr(forwards._Flags, condition);
            _Value = ConditionalSelect(As<TFlag, TValue>(forwards._Flags), forwards._Value, then);
        }

        public Vector<TValue> Else(in Vector<TValue> then)
            => ConditionalSelect(As<TFlag, TValue>(_Flags), _Value, then);
    }

    public static Vector<TTo> As<TFrom, TTo>(in Vector<TFrom> vector)
        where TFrom : unmanaged
        where TTo : unmanaged
        => Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(vector));

    public static ConditionalSelector<TFlag, TValue> If<TFlag, TValue>(in Vector<TFlag> condition, in Vector<TValue> then)
        where TFlag : unmanaged
        where TValue : unmanaged
        => Vector<TFlag>.Count == Vector<TValue>.Count
            ? new(condition, then)
            : throw new ArgumentException(string.Empty);

    public static ConditionalSelector<TFlag, TValue> Elif<TFlag, TValue>(
        this in ConditionalSelector<TFlag, TValue> @this,
        in Vector<TFlag> condition,
        in Vector<TValue> then)
        where TFlag : unmanaged
        where TValue : unmanaged
        => new(@this, condition, then);
}
