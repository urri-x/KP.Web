using System;
using System.Linq;

namespace KP.WebApi.Query
{
    public interface IQuery<T> where T : class 
    {
        IQueryable<T> All();
        T One(int id);
    }

    public interface IQueryWithDate<T> where T : class
    {
        IQueryable<T> All(DateTime date);
        T One(int id, DateTime date);
    }
}