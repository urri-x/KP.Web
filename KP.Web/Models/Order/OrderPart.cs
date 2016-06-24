using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using JetBrains.Annotations;

namespace KP.WebApi.Models.Order
{
    public class OrderPart
    {
        public string Name;
        public bool Visible = true;

        [NotNull]
        private string caption;

        public List<OrderField> Fields;
        public IQueryable<OrderRecord> Records;

        public OrderPart()
        {
            Fields=new List<OrderField>();
            Records = Enumerable.Empty<OrderRecord>().AsQueryable();
        }

        public string Caption
        {
            get
            {
                return !String.IsNullOrEmpty(caption)  ? caption: Name;
            }

            set
            {
                caption = value;
            }
        }
    }

    public class OrderRecord
    {
        public int Id;
        public List<OrderFieldValue> Fields;

        public OrderFieldValue this[string index]
        {
            get
            {
                var orderField = Fields.FirstOrDefault(x => x.Name.ToLower().Equals(index.ToLower()));
                return orderField != null ? orderField : new OrderFieldValue();
            }
            //set { Fields.SingleOrDefault(x => index.Equals(x.Name)).Value = value; }
        }
    }

}