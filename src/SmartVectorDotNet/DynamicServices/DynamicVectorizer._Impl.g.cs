#nullable enable
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace SmartVectorDotNet.DynamicServices
{
    #region delegates
    public delegate void VectorFunc1<T>(ReadOnlySpan<T> x1, Span<T> result);
    public delegate void VectorFunc2<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> result);
    public delegate void VectorFunc3<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> result);
    public delegate void VectorFunc4<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> result);
    public delegate void VectorFunc5<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> result);
    public delegate void VectorFunc6<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> result);
    public delegate void VectorFunc7<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> result);
    public delegate void VectorFunc8<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> result);
    public delegate void VectorFunc9<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> result);
    public delegate void VectorFunc10<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> result);
    public delegate void VectorFunc11<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, Span<T> result);
    public delegate void VectorFunc12<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, Span<T> result);
    public delegate void VectorFunc13<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, Span<T> result);
    public delegate void VectorFunc14<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, Span<T> result);
    public delegate void VectorFunc15<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, ReadOnlySpan<T> x15, Span<T> result);
    public delegate void VectorFunc16<T>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, ReadOnlySpan<T> x15, ReadOnlySpan<T> x16, Span<T> result);
    #endregion


    partial interface IDynamicVectorizer
    {
        VectorFunc1<T> Vectorize<T>(Expression<Func<T, T>> expression)
            where T : unmanaged;

        VectorFunc2<T> Vectorize<T>(Expression<Func<T, T, T>> expression)
            where T : unmanaged;

        VectorFunc3<T> Vectorize<T>(Expression<Func<T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc4<T> Vectorize<T>(Expression<Func<T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc5<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc6<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc7<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc8<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc9<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc10<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc11<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc12<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc13<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc14<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc15<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

        VectorFunc16<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged;

    }


    partial class DynamicVectorizer : IDynamicVectorizer
    {
        private static void GuardSpanSize<T>(Span<T> result, ReadOnlySpan<T> operand, string operandName)
        {
            if(result.Length > operand.Length)
            {
                throw new ArgumentException($"Any length of operands must be longer than one of result span. '{operandName}' is too short.");
            }
        }

        public VectorFunc1<T> Vectorize<T>(Expression<Func<T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc1<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i]);
                }
            }

            return core;
        }

        public VectorFunc2<T> Vectorize<T>(Expression<Func<T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc2<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i]);
                }
            }

            return core;
        }

        public VectorFunc3<T> Vectorize<T>(Expression<Func<T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc3<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i]);
                }
            }

            return core;
        }

        public VectorFunc4<T> Vectorize<T>(Expression<Func<T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc4<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i]);
                }
            }

            return core;
        }

        public VectorFunc5<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc5<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i]);
                }
            }

            return core;
        }

        public VectorFunc6<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc6<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i]);
                }
            }

            return core;
        }

        public VectorFunc7<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc7<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i]);
                }
            }

            return core;
        }

        public VectorFunc8<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc8<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i]);
                }
            }

            return core;
        }

        public VectorFunc9<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc9<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i]);
                }
            }

            return core;
        }

        public VectorFunc10<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc10<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i]);
                }
            }

            return core;
        }

        public VectorFunc11<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc11<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i]);
                }
            }

            return core;
        }

        public VectorFunc12<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc12<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                GuardSpanSize(result, x12, "x12");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX12 = MemoryMarshal.Cast<T, Vector<T>>(x12);
                var remainX12 = x12.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorX12, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i], remainX12[i]);
                }
            }

            return core;
        }

        public VectorFunc13<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc13<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                GuardSpanSize(result, x12, "x12");
                GuardSpanSize(result, x13, "x13");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX12 = MemoryMarshal.Cast<T, Vector<T>>(x12);
                var remainX12 = x12.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX13 = MemoryMarshal.Cast<T, Vector<T>>(x13);
                var remainX13 = x13.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorX12, vectorX13, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i], remainX12[i], remainX13[i]);
                }
            }

            return core;
        }

        public VectorFunc14<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc14<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                GuardSpanSize(result, x12, "x12");
                GuardSpanSize(result, x13, "x13");
                GuardSpanSize(result, x14, "x14");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX12 = MemoryMarshal.Cast<T, Vector<T>>(x12);
                var remainX12 = x12.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX13 = MemoryMarshal.Cast<T, Vector<T>>(x13);
                var remainX13 = x13.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX14 = MemoryMarshal.Cast<T, Vector<T>>(x14);
                var remainX14 = x14.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorX12, vectorX13, vectorX14, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i], remainX12[i], remainX13[i], remainX14[i]);
                }
            }

            return core;
        }

        public VectorFunc15<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc15<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, ReadOnlySpan<T> x15, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                GuardSpanSize(result, x12, "x12");
                GuardSpanSize(result, x13, "x13");
                GuardSpanSize(result, x14, "x14");
                GuardSpanSize(result, x15, "x15");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX12 = MemoryMarshal.Cast<T, Vector<T>>(x12);
                var remainX12 = x12.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX13 = MemoryMarshal.Cast<T, Vector<T>>(x13);
                var remainX13 = x13.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX14 = MemoryMarshal.Cast<T, Vector<T>>(x14);
                var remainX14 = x14.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX15 = MemoryMarshal.Cast<T, Vector<T>>(x15);
                var remainX15 = x15.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorX12, vectorX13, vectorX14, vectorX15, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i], remainX12[i], remainX13[i], remainX14[i], remainX15[i]);
                }
            }

            return core;
        }

        public VectorFunc16<T> Vectorize<T>(Expression<Func<T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T>> expression)
            where T : unmanaged
        {
            var scalarFunc = expression.Compile();
            var vectorizationVisitor = CreateVisitor();
            var vectorExpr = (LambdaExpression)vectorizationVisitor.Visit(expression);
            var alignedSpanExpr = ToSpanFunc<VectorFunc16<Vector<T>>>(vectorExpr);
            var alignedSpanFunc = alignedSpanExpr.Compile();

            void core(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, ReadOnlySpan<T> x11, ReadOnlySpan<T> x12, ReadOnlySpan<T> x13, ReadOnlySpan<T> x14, ReadOnlySpan<T> x15, ReadOnlySpan<T> x16, Span<T> result)
            {
                GuardSpanSize(result, x1, "x1");
                GuardSpanSize(result, x2, "x2");
                GuardSpanSize(result, x3, "x3");
                GuardSpanSize(result, x4, "x4");
                GuardSpanSize(result, x5, "x5");
                GuardSpanSize(result, x6, "x6");
                GuardSpanSize(result, x7, "x7");
                GuardSpanSize(result, x8, "x8");
                GuardSpanSize(result, x9, "x9");
                GuardSpanSize(result, x10, "x10");
                GuardSpanSize(result, x11, "x11");
                GuardSpanSize(result, x12, "x12");
                GuardSpanSize(result, x13, "x13");
                GuardSpanSize(result, x14, "x14");
                GuardSpanSize(result, x15, "x15");
                GuardSpanSize(result, x16, "x16");
                var vectorResult = MemoryMarshal.Cast<T, Vector<T>>(result);
                var remainResult = result.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
                var remainX1 = x1.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
                var remainX2 = x2.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
                var remainX3 = x3.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
                var remainX4 = x4.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
                var remainX5 = x5.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
                var remainX6 = x6.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
                var remainX7 = x7.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
                var remainX8 = x8.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
                var remainX9 = x9.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
                var remainX10 = x10.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX11 = MemoryMarshal.Cast<T, Vector<T>>(x11);
                var remainX11 = x11.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX12 = MemoryMarshal.Cast<T, Vector<T>>(x12);
                var remainX12 = x12.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX13 = MemoryMarshal.Cast<T, Vector<T>>(x13);
                var remainX13 = x13.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX14 = MemoryMarshal.Cast<T, Vector<T>>(x14);
                var remainX14 = x14.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX15 = MemoryMarshal.Cast<T, Vector<T>>(x15);
                var remainX15 = x15.Slice(Vector<T>.Count * vectorResult.Length);
                var vectorX16 = MemoryMarshal.Cast<T, Vector<T>>(x16);
                var remainX16 = x16.Slice(Vector<T>.Count * vectorResult.Length);

                alignedSpanFunc(vectorX1, vectorX2, vectorX3, vectorX4, vectorX5, vectorX6, vectorX7, vectorX8, vectorX9, vectorX10, vectorX11, vectorX12, vectorX13, vectorX14, vectorX15, vectorX16, vectorResult);
                for(var i = 0; i < remainResult.Length; ++i)
                {
                    remainResult[i] = scalarFunc(remainX1[i], remainX2[i], remainX3[i], remainX4[i], remainX5[i], remainX6[i], remainX7[i], remainX8[i], remainX9[i], remainX10[i], remainX11[i], remainX12[i], remainX13[i], remainX14[i], remainX15[i], remainX16[i]);
                }
            }

            return core;
        }

    }
}
