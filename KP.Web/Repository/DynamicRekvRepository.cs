using System.Data.Linq;
using System.Linq;
using KP.WebApi.Controllers;
using KP.WebApi.Repository.Entity;

namespace KP.WebApi.Repository
{
    public abstract class DynamicRekvRepository : IRepository<DynamicRekv>
    {
        private readonly KPDataContext dbContext;

        protected DynamicRekvRepository(DataContext dbContext=null)
        {
            this.dbContext = (KPDataContext)(dbContext ?? Db.GetDbContext());
        }

        public IQueryable<DynamicRekv> GetAll()
        {
            return from ctx in dbContext.dynamic_rekvs
            select new DynamicRekv()
            {
                Id = ctx.id,
                ObjectId = ctx.object_id.Value,
                IdRekv = ctx.ID_AppObjRekv.Value,
                DateBegin = ctx.date_begin.Value,
                AsString = ctx.charval,
                AsBool = ctx.boolval.Value,
                AsDate = ctx.dateval.Value,
                AsDouble = ctx.floatval.Value
            };
        }

        public DynamicRekv GetById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id.Equals(id));
        }

        public int Update(DynamicRekv entity)
        {
            throw new System.NotImplementedException();
        }

        public DynamicRekv Add(DynamicRekv entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}