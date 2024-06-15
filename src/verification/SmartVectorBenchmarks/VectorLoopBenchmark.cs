using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorBenchmarks;

public class VectorLoopBenchmark
{
    internal class VectorLoop
    {
        private static class Const<T>
            where T : unmanaged
        {
            public static readonly Vector<T> TrueValue = Vector.Equals(Vector<T>.Zero, Vector<T>.Zero);
            public static readonly Vector<T> FalseValue = Vector.Equals(Vector<T>.Zero, Vector<T>.One);
        }

        public interface ILoopOp2<T> where T : unmanaged
        {
            public void Condition(in Vector<T> value1, in Vector<T> value2, out Vector<T> condition);
            public void Statement(ref Vector<T> value1, ref Vector<T> value2);
        }

        public struct Sum : ILoopOp2<ulong>
        {
            public readonly Vector<ulong> N;

            public Sum(in Vector<ulong> n) { N = n; }

            public void Condition(in Vector<ulong> i, in Vector<ulong> x, out Vector<ulong> condition)
                => condition = Vector.LessThan(i, N);

            public void Statement(ref Vector<ulong> i, ref Vector<ulong> x)
            {
                i += Vector<ulong>.One;
                x += i;
            }
        }


        public static Vector<ulong> ByScalar(in Vector<ulong> n)
        {
            var x = (stackalloc ulong[Vector<ulong>.Count]);
            for (var i = 0; i < Vector<ulong>.Count; ++i)
            {
                for (var j = 0uL; j < n[i];)
                {
                    ++j;
                    x[i] += j;
                }
            }
            return Unsafe.As<ulong, Vector<ulong>>(ref x[0]);
        }


        public static Vector<ulong> SolidImpl(in Vector<ulong> n)
        {
            var i = Vector<ulong>.Zero;
            var x = Vector<ulong>.Zero;
            while(true)
            {
                var condition = Vector.LessThan(i, n);
                if (Vector.EqualsAll(condition, Const<ulong>.FalseValue))
                {
                    return x;
                }
                i = Vector.ConditionalSelect(condition, i + Vector<ulong>.One, i);
                x = Vector.ConditionalSelect(condition, x + i, x);
            }
        }


        public static Vector<ulong> GenericStrategy(in Vector<ulong> n)
        {
            var sum = new Sum(n);
            var i = Vector<ulong>.Zero;
            var x = Vector<ulong>.Zero;
            Loop(ref sum, ref i, ref x);
            return x;
        }


        public static void Loop<TStrategy, TValue>(ref TStrategy strategy, ref Vector<TValue> value1, ref Vector<TValue> value2)
            where TStrategy : struct, ILoopOp2<TValue>
            where TValue : unmanaged
        {
            while (true)
            {
                strategy.Condition(value1, value2, out var condition);
                if (Vector.EqualsAll(condition, Const<TValue>.FalseValue))
                {
                    return;
                }
                var v1Temp = value1;
                var v2Temp = value2;
                strategy.Statement(ref v1Temp, ref v2Temp);
                value1 = Vector.ConditionalSelect(condition, v1Temp, value1);
                value2 = Vector.ConditionalSelect(condition, v2Temp, value2);
            }
        }
    }


    public static readonly Vector<ulong>[] Values
        = Enumerable
        .Range(0, 256)
        .Select(x => new Vector<ulong>((ulong)x))
        .ToArray();


    public VectorLoopBenchmark()
    {
        var useByScalar = UseByScalar();
        var useSolidImpl = UseSolidImpl();
        var useGenericStrategy = UseGenericStrategy();
        if(useByScalar != useSolidImpl)
        {
            throw new InvalidOperationException();
        }
        if(useByScalar != useGenericStrategy)
        {
            throw new InvalidOperationException();
        }
    }


    [Benchmark]
    public Vector<ulong> UseByScalar()
    {
        var retval = Vector<ulong>.Zero;
        foreach (var value in Values)
        {
            retval += VectorLoop.ByScalar(value);
        }
        return retval;
    }


    [Benchmark]
    public Vector<ulong> UseSolidImpl()
    {
        var retval = Vector<ulong>.Zero;
        foreach(var value in Values)
        {
            retval += VectorLoop.SolidImpl(value);
        }
        return retval;
    }


    [Benchmark]
    public Vector<ulong> UseGenericStrategy()
    {
        var retval = Vector<ulong>.Zero;
        foreach (var value in Values)
        {
            retval += VectorLoop.GenericStrategy(value);
        }
        return retval;
    }
}
