using System.Linq;
using KP.WebApi.Models;
using KP.WebApi.Models.Order;

namespace KP.WebApi.Query
{
    public static class OrdersQueryExtensions
    {
        public static IQueryable<Order> Accepted(this IQueryable<Order> q)
        {
            return q.Where(o => o.Status.Equals(1));
        }

    }
}