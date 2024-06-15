using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet.PoC;

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
        public Vector<T> Condition(in Vector<T> value1, in Vector<T> value2);
        public void Statement(ref Vector<T> value1, ref Vector<T> value2);
    }

    public struct Sum : ILoopOp2<int>
    {
        public readonly Vector<int> N;

        public Sum(in Vector<int> n) {  N = n; }

        public Vector<int> Condition(in Vector<int> i, in Vector<int> x)
            => Vector.LessThan(i, N);

        public void Statement(ref Vector<int> i, ref Vector<int> x)
        {
            i += Vector<int>.One;
            x += i;
        }
    }


    public static Vector<int> SolidImpl(in Vector<int> n)
    {
        var x = (stackalloc int[Vector<int>.Count]);
        for(var i = 0; i < Vector<int>.Count; ++i)
        {
            for(var j = 0; j < n[i];)
            {
                ++j;
                x[i] += j;
            }
        }
        return Unsafe.As<int, Vector<int>>(ref x[0]);
    }


    public static Vector<int> Generics(in Vector<int> n)
    {
        var sum = new Sum(n);
        var i = Vector<int>.Zero;
        var x = Vector<int>.Zero;
        Loop(ref sum, ref i, ref x);
        return x;
    }


    public static void Loop<TStrategy, TValue>(ref TStrategy strategy, ref Vector<TValue> value1, ref Vector<TValue> value2)
        where TStrategy : struct, ILoopOp2<TValue>
        where TValue : unmanaged
    {
        while(true)
        {
            var condition = strategy.Condition(value1, value2);
            if(Vector.EqualsAll(condition, Const<TValue>.FalseValue))
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
