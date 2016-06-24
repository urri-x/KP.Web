using System;
using System.Data.Linq;
using System.Linq;
using KP.WebApi.Controllers;
using KP.WebApi.Helpers;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{
    public class PodrQuery : IQueryWithDate<Podr>
    {
        private readonly KPDataContext dataContext;

        public PodrQuery(DataContext dataContext = null)
        {
            this.dataContext = (KPDataContext)(dataContext ?? Db.GetDbContext());
        }

        public IQueryable<Podr> All(DateTime date)
        {
            var query =
                from o in dataContext.objects
                join p in dataContext.const_rekvs on o.id equals p.object_id
                join pObject in dataContext.objects on p.intval equals pObject.id
                where o.id_type_object.Equals(PodrRekvConstants.Podr)
                      && p.ID_AppObjRekv.Equals(PodrRekvConstants.ParentObject)
                select new Podr()
                {
                    Id = o.id,
                    IdParent = p.intval.Value,
                    ParentType = pObject.id_type_object,
                    Shortname = (from rekv in dataContext.dynamic_rekvs
                        where
                            rekv.ID_AppObjRekv.Equals(PodrRekvConstants.Shortname) && rekv.object_id.Equals(o.id) &&
                            rekv.date_begin <= date
                        orderby rekv.date_begin descending
                        select rekv.charval).FirstOrDefault(),
                };
            return query;
        }

        public Podr One(int id, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}