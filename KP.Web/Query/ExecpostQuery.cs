using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using KP.WebApi.Controllers;
using KP.WebApi.Helpers;
using KP.WebApi.Models;

namespace KP.WebApi.Query
{
    public static class ExecpostQueryExtensions
    {
        public static IQueryable<Execpost> FromDepartment(this IQueryable<Execpost> q, int IdPodr)
        {
            return q.Where(e => e.IdTopPodr.Equals(IdPodr));
        }
        public static IQueryable<Execpost> Alive(this IQueryable<Execpost> q, DateTime date)
        {
            return q.Where(e => e.DateBegin<=date
                && (e.DateEnd == null || e.DateEnd >= date));
        }

    }

    public class ExecpostQuery : QueryBase<Execpost>, IQuery<Execpost>
    {
        private readonly KPDataContext dataContext;
        public ExecpostQuery Alive(DateTime date)
        {
            AddCriteria(e => e.DateBegin <= date);
            AddCriteria(e => e.DateEnd == null || e.DateEnd >= date);
            return this;
        }

        public ExecpostQuery(DataContext dataContext = null)
        {
            this.dataContext = (KPDataContext)(dataContext ?? Db.GetDbContext());
        }


        public IQueryable<Execpost> All()
        {
            var parentsQuery = GetParentsQuery();

            var query = from rekv in dataContext.constlist_rekvs
                join exec in dataContext.PERSONAL_EXECPOSTs on rekv.id equals exec.id1

                join p in parentsQuery on rekv.intval equals p.Id into group1
                from g1 in group1.DefaultIfEmpty()

                join p2 in parentsQuery on g1.IdParent equals p2.Id into group2
                from g2 in group2.DefaultIfEmpty()

                join p3 in parentsQuery on g2.IdParent equals p3.Id into group3
                from g3 in group3.DefaultIfEmpty()

                join p4 in parentsQuery on g3.IdParent equals p4.Id into group4
                from g4 in group4.DefaultIfEmpty()

                join p5 in parentsQuery on g4.IdParent equals p5.Id into group5
                from g5 in group5.DefaultIfEmpty()

                join p6 in parentsQuery on g5.IdParent equals p6.Id into group6
                from g6 in group6.DefaultIfEmpty()

                join p7 in parentsQuery on g6.IdParent equals p7.Id into group7
                from g7 in group7.DefaultIfEmpty()

                join p8 in parentsQuery on g7.IdParent equals p8.Id into group8
                from g8 in group8.DefaultIfEmpty()


                where rekv.ID_AppObjRekv.Equals(PersonalRekvConstants.Execpost)
                select new Execpost()
                {
                    Id = rekv.id,
                    IdPersonal = rekv.object_id.Value,
                    IdShtat = rekv.intval.Value,
                    DateBegin = rekv.dateval.Value,
                    DateEnd = exec.date_end.Value,
                    IdWork = exec.id_work.Value,

                    IdTopPodr = g1.ParentType==3 && g2.ParentType==2 ? g1.IdParent : 
                        g2.ParentType == 3 && g3.ParentType == 2 ? g2.IdParent :
                        g3.ParentType == 3 && g4.ParentType == 2 ? g3.IdParent :
                        g4.ParentType == 3 && g5.ParentType == 2 ? g4.IdParent :
                        g5.ParentType == 3 && g6.ParentType == 2 ? g5.IdParent :
                        g6.ParentType == 3 && g7.ParentType == 2 ? g6.IdParent :
                        g7.ParentType == 3 && g8.ParentType == 2 ? g7.IdParent : 
                        g8.ParentType==3? g8.IdParent:0,
                    IdFirm = g1.ParentType==2 ? g1.IdParent:
                        g2.ParentType == 2 ? g2.IdParent :
                        g3.ParentType == 2 ? g3.IdParent :
                        g4.ParentType == 2 ? g4.IdParent :
                        g5.ParentType == 2 ? g5.IdParent :
                        g6.ParentType == 2 ? g6.IdParent :
                        g7.ParentType == 2 ? g7.IdParent :
                        g8.ParentType == 2 ? g8.IdParent : 0
                };
            return query;
        }

        public Execpost One(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ObjectWithParent> GetParentsQuery()
        {
            var parentRekvIds =  from r in dataContext.rekvs
                   where r.RekvName.Equals("parent_object") && (r.ObjTypeID.Equals(3) || r.ObjTypeID.Equals(4) || r.ObjTypeID.Equals(2))
                                 select r.id;

            var parents =  from rekv in dataContext.const_rekvs
                    join parentObjects in dataContext.objects on rekv.intval equals parentObjects.id
                    join objects in dataContext.objects on rekv.object_id equals objects.id
                    where parentRekvIds.Contains(rekv.ID_AppObjRekv.Value)
                select
                    new ObjectWithParent()
                    {
                        Id = rekv.object_id.Value,
                        Type = objects.id_type_object,
                        IdParent = rekv.intval.Value,
                        ParentType = parentObjects.id_type_object
                    };
            return parents;
        }

    }
}
