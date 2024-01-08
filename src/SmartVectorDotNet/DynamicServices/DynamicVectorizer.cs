using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet.DynamicServices
{
    public partial class DynamicVectorizer : IDynamicVectorizer
    {
        private static T ReadOnlySpanGetValue<T>(ReadOnlySpan<T> span, int index) => span[index];
        private static void SpanSetValue<T>(Span<T> span, int index, T value) => span[index] = value;
        private static MethodInfo ReadOnlySpanGetValueMethod(Type type)
            => typeof(DynamicVectorizer)
                .GetMethod(nameof(ReadOnlySpanGetValue), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(type);
        private static MethodInfo SpanSetValueMethod(Type type)
            => typeof(DynamicVectorizer)
                .GetMethod(nameof(SpanSetValue), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(type);


        private VectorizationVisitor CreateVisitor()
            => new VectorizationVisitor(Enumerable.Empty<IMethodReplacementStrategy>());


        private Expression<T> ToSpanFunc<T>(LambdaExpression vectorFunc)
        {
            var spanType = typeof(Span<>).MakeGenericType(vectorFunc.ReturnType);
            var roSpanType = typeof(ReadOnlySpan<>).MakeGenericType(vectorFunc.ReturnType);

            var i = Expression.Parameter(typeof(int));
            var inParameterSpans = vectorFunc.Parameters.Select(p => Expression.Parameter(roSpanType, p.Name)).ToArray();
            var outParameterSpan = Expression.Parameter(spanType, "result");
            var visitor = new ParameterVectorizeVisitor(vectorFunc.ReturnType, i, vectorFunc.Parameters, inParameterSpans);
            var body = visitor.Visit(vectorFunc.Body);
            var block = Expression.Block(
                new ParameterExpression[] { i, },
                new Expression []
                {
                    ExpressionEx.For(i, Expression.Property(outParameterSpan, nameof(Span<int>.Length)), Expression.Block(
                        Expression.Call(
                            method: SpanSetValueMethod(vectorFunc.ReturnType),
                            arguments: new []{ outParameterSpan, i, body, }
                            )
                        )
                    )
                });
            return Expression.Lambda<T>(
                block, 
                inParameterSpans.Concat(new[] { outParameterSpan, })
                );
        }


        private sealed class ParameterVectorizeVisitor : ExpressionVisitor
        {
            private Dictionary<ParameterExpression, Expression> _parameterReplacementSet;

            public ParameterVectorizeVisitor(
                Type elementType,
                ParameterExpression i,
                ReadOnlyCollection<ParameterExpression> parameters,
                ParameterExpression[] inParameterSpans)
            {
                var getValueMethod = typeof(DynamicVectorizer).GetMethod(nameof(ReadOnlySpanGetValue)).MakeGenericMethod(elementType);
                _parameterReplacementSet = Enumerable
                    .Zip(parameters, inParameterSpans, (v, sp) => (v, sp))
                    .ToDictionary(
                    tpl => tpl.v,
                    tpl => (Expression)Expression.Call(getValueMethod, i)
                    );
            }

            protected override Expression VisitParameter(ParameterExpression node)
                => _parameterReplacementSet.TryGetValue(node, out var replacement) ? replacement : base.VisitParameter(node);
        }
    }
}
