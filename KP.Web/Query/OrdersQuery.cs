using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using KP.WebApi.Controllers;
using KP.WebApi.Helpers;
using KP.WebApi.Models;
using KP.WebApi.Models.Order;
using Topshelf.Configurators;

namespace KP.WebApi.Query
{
    public class OrdersQuery : IQuery<Order>
    {
        private readonly KPDataContext dataContext;

        public OrdersQuery(DataContext dataContext = null)
        {
            this.dataContext = (KPDataContext)(dataContext ?? Db.GetDbContext());
        }

        private List<OrderFieldValue> GetFields(int IdRecord)
        {
            return 
                (from v in dataContext.JRN_Orders_Records_Values
                   where v.id_record.Equals(IdRecord)
                select new OrderFieldValue()
                {
                    Name = v.FieldName,
                    Type =
                        v.intval.HasValue
                            ? OrderFieldType.Int
                            : v.floatval.HasValue
                                ? OrderFieldType.Float
                                : v.boolval.HasValue
                                    ? OrderFieldType.Bool
                                    : v.dateval.HasValue ? OrderFieldType.Date : OrderFieldType.String,
                    AsInt = v.intval,
                    AsDate = v.dateval,
                    AsString = v.charval,
                    AsFloat = v.floatval,
                    AsBool = v.boolval
                }).ToList();
        }
        
        public IQueryable<Order> All()
        {
            return 
                from o in dataContext.JRN_Orders
                join s in dataContext.ASysStoreItems on o.id_OrderType equals s.id
                join item in dataContext.REC_View_StoreItems on s.ItemName equals item.ItemName
                join @ref in dataContext.REFs on item.id1 equals @ref.ID
                select new Order()
                {
                    Id = o.id,
                    Number = o.number,
                    Type = @ref.ShortName,
                    Code = @ref.Code,
                    Status = o.status,
                    CreateDate = o.date_begin.Value,
                    AcceptDate = o.AcceptDate.Value,
                    Description = o.Note,
                    LastModifiedByUser = o.LastModifiedUser,
                    Parts = GetParts(o.id)
                };
        }

        private IQueryable<OrderRecord> GetRecords(int IdOrder, string PartName)
        {
            return from r in dataContext.JRN_Orders_Records 
            where r.id_order.Equals(IdOrder) && r.PartName.Equals(PartName)
            orderby r.id
            select new OrderRecord()
            {
                Id = r.id,
                Fields = GetFields(r.id)
            };
        }
        private OrderParts GetParts(int idOrder)
        {
            var distinctNames =
                (from r in dataContext.JRN_Orders_Records
                where r.id_order.Equals(idOrder)
                select r.PartName.ToString()
                ).Distinct();
            var orderParts = new OrderParts();
            foreach (var name in distinctNames)
            {
                var distinctFields =
                    (from r in dataContext.JRN_Orders_Records
                    join v in dataContext.JRN_Orders_Records_Values on r.id equals v.id_record
                    where r.id_order.Equals(idOrder) && r.PartName.Equals(name)
                    orderby v.id
                    select v.FieldName.ToString()
                    ).Distinct();
                orderParts[name]=new OrderPart()
                {
                    Name = name,
                    Fields = distinctFields.Select(f=> new OrderField()
                    {
                        Name = f
                    }).ToList(),
                    Records = GetRecords(idOrder, name)
                };
            }
            return orderParts;
        }


        public Order One(int id)
        {
            return All().SingleOrDefault(x => x.Id.Equals(id)).ApplyTemplate();
        }
    }
}