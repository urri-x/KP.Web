using System;
using System.Data.Linq;
using System.Linq;
using KP.WebApi.Controllers;
using KP.WebApi.Helpers;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{
    public class PersonalRestQuery : IQueryWithDate<PersonalRest>
    {
        private readonly KPDataContext dataContext;

        public PersonalRestQuery(DataContext dataContext = null)
        {
            this.dataContext = (KPDataContext)(dataContext ?? Db.GetDbContext());
        }

        public IQueryable<PersonalRest> All (DateTime date)
        {
            var restQuery = GetRestQuery();
            var restWithExecpostQuery = JoinResWithExecposts(restQuery, new ExecpostQuery(dataContext).All().Alive(date));
            return JoinPersonsWithRests(restWithExecpostQuery, new PersonalQuery(dataContext).All(date));
        }

        public PersonalRest One(int id, DateTime date)
        {
            throw new NotImplementedException();
        }

        private static IQueryable<PersonalRest> JoinPersonsWithRests(IQueryable<Rest> restWithExecpostQuery, IQueryable<Person> personQuery)
        {
            return restWithExecpostQuery.Join(personQuery,
                rest => rest.Id,
                person => person.Id,
                (rest, person) => new PersonalRest()
                {
                    Tabn = person.Tabn,
                    Fio = person.Fio,
                    RestDateBegin = rest.RestDateBegin,
                    RestDateEnd = rest.RestDateEnd,
                    RestType = rest.RestType,
                    IdPodr = rest.IdPodr
                });
        }

        private static IQueryable<Rest> JoinResWithExecposts(IQueryable<Rest> restQuery, IQueryable<Execpost> execpostQuery)
        {
            return restQuery.Join(execpostQuery,
                rest => rest.IdWork,
                exec => exec.IdWork,
                (rest, exec) => new Rest()
                {
                    Id = rest.Id,
                    IdWork = rest.IdWork,
                    IdPodr = exec.IdTopPodr,
                    RestDateBegin = rest.RestDateBegin,
                    RestDateEnd = rest.RestDateEnd,
                    RestType = rest.RestType
                });
        }

        private IQueryable<Rest> GetRestQuery()
        {
            return from rekv in dataContext.constlist_rekvs
                   join rest in dataContext.PERSONAL_REST_TAKENs on rekv.id equals rest.id1
                   join band in dataContext.dynamiclist_rekvs on rest.id_band equals band.id
                   join bandTail in dataContext.PERSONAL_RESTs on band.id equals bandTail.id1
                   join @ref in dataContext.REFs on band.intval equals @ref.ID

                   select new Rest()
                   {
                       Id = rekv.object_id.Value,
                       IdWork = bandTail.id_work.Value,
                       RestDateBegin = rest.date_begin.Value,
                       RestDateEnd = rekv.dateval.Value,
                       RestType = @ref.FullName
                   };
        }
    }
}