using System;
using System.Data.Linq;
using System.Linq;
using KP.WebApi.Controllers;
using KP.WebApi.Helpers;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{

    public class PersonalQuery : IQueryWithDate<Person>
    {

        private readonly KPDataContext dataContext;

        public PersonalQuery(DataContext dataContext=null)
        {
            this.dataContext = (KPDataContext)(dataContext ?? Db.GetDbContext());
        }

        public IQueryable<Person> All(DateTime date)
        {
            KPDataContext db = dataContext;
            var query =
                from o in db.objects
                where o.id_type_object.Equals(PersonalRekvConstants.Personal) 
                select new Person()
                {
                    Id = o.id,
                    Tabn = (from rekv in db.dynamic_rekvs  
                            where rekv.ID_AppObjRekv.Equals(PersonalRekvConstants.Tabn) && rekv.object_id.Equals(o.id) && rekv.date_begin<=date
                            orderby rekv.date_begin descending 
                            select rekv.charval).FirstOrDefault(),

                    Surname = (from rekv in db.dynamic_rekvs
                            where rekv.ID_AppObjRekv.Equals(PersonalRekvConstants.Surname) && rekv.object_id.Equals(o.id) && rekv.date_begin <= date
                            orderby rekv.date_begin descending
                            select rekv.charval).FirstOrDefault(),

                    Name = (from rekv in db.dynamic_rekvs
                               where rekv.ID_AppObjRekv.Equals(PersonalRekvConstants.Name) && rekv.object_id.Equals(o.id) && rekv.date_begin <= date
                               orderby rekv.date_begin descending
                               select rekv.charval).FirstOrDefault(),

                    Patronymic = (from rekv in db.dynamic_rekvs
                               where rekv.ID_AppObjRekv.Equals(PersonalRekvConstants.Patronymic) && rekv.object_id.Equals(o.id) && rekv.date_begin <= date
                               orderby rekv.date_begin descending
                               select rekv.charval).FirstOrDefault()
                };
            return query;

        }

        public Person One(int id, DateTime date)
        {
            return All(date)
                .SingleOrDefault(x => x.Id.Equals(id));
        }
    }
}