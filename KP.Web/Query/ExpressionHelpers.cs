using System;
using System.Linq;
using System.Linq.Expressions;

namespace KP.WebApi.Query
{
    public static class ExpressionHelpers
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left,Expression<Func<T, bool>> right)
        {
            return BinaryOnExpressions(left, ExpressionType.AndAlso, right);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> left,Expression<Func<T, bool>> right)
        {
            return BinaryOnExpressions(left, ExpressionType.OrElse, right);
        }


        public static Expression<Func<T, bool>> BinaryOnExpressions<T>(this Expression<Func<T, bool>> left,ExpressionType binaryType,Expression<Func<T, bool>> right)
        {
            var rightInvoke = Expression.Invoke(right, left.Parameters.Cast<Expression>());
            var binExpression = Expression.MakeBinary(binaryType, left.Body, rightInvoke);
            return Expression.Lambda<Func<T, bool>>(binExpression, left.Parameters);
        }
    }
}