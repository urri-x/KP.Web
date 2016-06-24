using System;
using System.Linq.Expressions;

namespace KP.WebApi.Query
{
    public abstract class QueryBase<TEntity> 
    {
        private Expression<Func<TEntity, bool>> curExpression;

        public Expression<Func<TEntity, bool>> AsExpression()
        {
            return curExpression;
        }

        public Expression<Func<TEntity, bool>> AndAlso(QueryBase<TEntity> otherQuery)
        {
            return AsExpression().AndAlso(otherQuery.AsExpression());
        }

        public Expression<Func<TEntity, bool>> OrElse(QueryBase<TEntity> otherQuery)
        {
            return AsExpression().OrElse(otherQuery.AsExpression());
        }

        protected void AddCriteria(Expression<Func<TEntity, bool>> nextExpression)
        {
            curExpression = (curExpression == null)
                ? nextExpression
                : curExpression.AndAlso(nextExpression);
        }
        protected void AddOrCriteria(Expression<Func<TEntity, bool>> nextExpression)
        {
            curExpression = (curExpression == null)
                ? nextExpression
                : curExpression.OrElse(nextExpression);
        }

    }
}