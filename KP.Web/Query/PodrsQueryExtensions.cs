using System.Linq;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{
    public static class PodrsQueryExtensions
    {
        public static IQueryable<Podr> TopLevel(this IQueryable<Podr> q)
        {
            return q.Where(x => x.ParentType.Equals(FirmekvConstants.Firm));
        }
    }
}