using System;
using System.Linq;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{
    public static class PersonalRestQueryExtensions
    {
        public static IQueryable<PersonalRest> OnDate(this IQueryable<PersonalRest> q, DateTime date)
        {
            return q.Where(x => x.RestDateBegin <= date && x.RestDateEnd >= date);
        }
        public static IQueryable<PersonalRest> ForPodr(this IQueryable<PersonalRest> q, int idPodr)
        {
            return q.Where(x => x.IdPodr.Equals(idPodr));
        }
    }
}