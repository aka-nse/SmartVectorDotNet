using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet.DynamicServices
{
    // convert function for scalar T  to fo System.Numerics.Vector<T>.
    internal sealed class VectorizationVisitor : ExpressionVisitor
    {
        private static MethodInfo GetVectorValue_Opened { get; }
            = typeof(VectorizationVisitor)
                .GetMethod(nameof(GetVectorValue), BindingFlags.NonPublic | BindingFlags.Static);
        private static T GetVectorValue<T>(in Vector<T> vector, int index)
            where T : unmanaged
            => vector[index];

        private readonly IEnumerable<IMethodReplacementStrategy> _methodReplacementStrategies;
        private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterReplacementMap = new();

        public IReadOnlyDictionary<ParameterExpression, ParameterExpression> ParameterReplacementMap => _parameterReplacementMap;

        public VectorizationVisitor(IEnumerable<IMethodReplacementStrategy> methodReplacementStrategies)
            => _methodReplacementStrategies = methodReplacementStrategies;

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (!typeof(Delegate).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentException();
            }
            var elementType = node.ReturnType;
            var originalParameterTypes = node.Parameters.Select(p => p.Type).ToArray();
            if (originalParameterTypes.Any(type => type != elementType))
            {
                throw new NotSupportedException();
            }
            var visitedDelegateType = (originalParameterTypes.Length switch
            {
                01 => typeof(Func<,>),
                02 => typeof(Func<,,>),
                03 => typeof(Func<,,,>),
                04 => typeof(Func<,,,,>),
                05 => typeof(Func<,,,,,>),
                06 => typeof(Func<,,,,,,>),
                07 => typeof(Func<,,,,,,,>),
                08 => typeof(Func<,,,,,,,,>),
                09 => typeof(Func<,,,,,,,,,>),
                10 => typeof(Func<,,,,,,,,,,>),
                11 => typeof(Func<,,,,,,,,,,,>),
                12 => typeof(Func<,,,,,,,,,,,,>),
                13 => typeof(Func<,,,,,,,,,,,,,>),
                14 => typeof(Func<,,,,,,,,,,,,,,>),
                15 => typeof(Func<,,,,,,,,,,,,,,,>),
                16 => typeof(Func<,,,,,,,,,,,,,,,,>),
                _ => throw new NotSupportedException(),
            }).MakeGenericType(originalParameterTypes.Concat(new[] { elementType }).Select(type => typeof(Vector<>).MakeGenericType(type)).ToArray());
            var lambda = Expression.Lambda(
                delegateType: visitedDelegateType,
                body: Visit(node.Body),
                parameters: node.Parameters.Select(p => (ParameterExpression)VisitParameter(p)));
            return lambda;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            foreach (var strategy in _methodReplacementStrategies)
            {
                if (strategy.TryReplaceTo(node.Method, out var replaceTo))
                {
                    return Expression.Call(node.Object, replaceTo, node.Arguments.Select(Visit));
                }
            }

            var containerSize = typeof(Vector<>)
                .MakeGenericType(node.Type)
                .GetProperty(nameof(Vector<int>.Count), BindingFlags.Public | BindingFlags.Static)
                .GetValue(null)
                is int x ? x : throw new NotSupportedException();
            var containerType = node.Type;
            for (var size = containerSize; size > 1; size >>= 1)
            {
                containerType = typeof(SumContainer<>).MakeGenericType(containerType);
            }
            var result = Expression.Parameter(typeof(MutableVector<,>).MakeGenericType(node.Type, containerType));
            var args = node.Arguments.Select(arg => Expression.Parameter(typeof(Vector<>).MakeGenericType(arg.Type))).ToArray();
            var argsAssign = Enumerable.Zip(args, node.Arguments.Select(Visit), (arg, value) => (Expression)Expression.Assign(arg, value)).ToArray();
            var block = Expression.Block(
                args.Concat(new ParameterExpression[] { result }),
                argsAssign.Concat(
                    Enumerable
                        .Range(0, containerSize)
                        .Select(i =>
                        {
                            var ii = Expression.Constant(i);
                            var getValue = Expression.Call(node.Object, node.Method, args.Select(arg => Expression.Call(GetVectorValue_Opened.MakeGenericMethod(arg.Type.GenericTypeArguments[0]), arg, ii)));
                            return Expression.Call(result, result.Type.GetMethod(nameof(MutableVector<int, int>.SetValue), BindingFlags.Public | BindingFlags.Instance), ii, getValue);
                        })
                    ).Concat(new Expression[]
                        {
                            Expression.Call(result, result.Type.GetMethod(nameof(MutableVector<int, int>.ToVector)))
                        }
                    ).ToArray()
                );
            return block;
        }

        protected override Expression VisitUnary(UnaryExpression node)
            => Expression.MakeUnary(node.NodeType, Visit(node.Operand), typeof(Vector<>).MakeGenericType(node.Type));

        protected override Expression VisitBinary(BinaryExpression node)
            => Expression.MakeBinary(node.NodeType, Visit(node.Left), Visit(node.Right));

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (!_parameterReplacementMap.TryGetValue(node, out var replacement))
            {
                replacement = Expression.Parameter(typeof(Vector<>).MakeGenericType(node.Type), node.Name);
                _parameterReplacementMap.Add(node, replacement);
            }
            return replacement;
        }

        protected override Expression VisitConstant(ConstantExpression node)
            => Expression.Constant(Activator.CreateInstance(typeof(Vector<>).MakeGenericType(node.Type), new object[] { node.Value, }));


        private unsafe struct MutableVector<T, TContainer>
            where T : unmanaged
            where TContainer : unmanaged
        {
            public static int Count => sizeof(TContainer) / sizeof(T);

            private TContainer _container;

            public T GetValue(int index) => *((T*)Unsafe.AsPointer(ref _container) + index);
            public void SetValue(int index, T value) => *((T*)Unsafe.AsPointer(ref _container) + index) = value;

            public Vector<T> ToVector() => Unsafe.As<TContainer, Vector<T>>(ref _container);
        }

        private struct SumContainer<T>
            where T : unmanaged
        {
            // These fields are alignment place holders.
            // Do not remove.
#pragma warning disable CS0169,IDE0044,IDE0051
            private T _field1;
            private T _field2;
#pragma warning restore CS0169,IDE0044,IDE0051
        }
    }

}
