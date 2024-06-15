using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SmartVectorDotNet.DynamicServices
{
    internal static class ExpressionEx
    {
        public static Expression For(ParameterExpression counter, Expression toExclusive, Expression body)
        {
            var endFor = Expression.Label();
            return Expression.Block(
                Expression.Assign(counter, Expression.Constant(0, typeof(int))),
                Expression.Loop(
                    Expression.Block(
                        Expression.IfThen(
                            Expression.Not(Expression.LessThan(counter, toExclusive)),
                            Expression.Break(endFor)
                            ),
                        body,
                        Expression.PreIncrementAssign(counter)
                    ),
                    endFor)
                );
        }
    }
}
