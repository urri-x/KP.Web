using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KP.WebApi.Models.Order
{
    public class OrderParts :IEnumerable<OrderPart>
    {
        private Dictionary<string, OrderPart> parts;

        public OrderParts()
        {
            parts = new Dictionary<string, OrderPart>();
        }

        public OrderParts(Dictionary<string, OrderPart> parts)
        {
            this.parts = parts;
        }


        public string[] Names => parts.Keys.ToArray();

        public OrderPart this[string index]
        {
            get
            {
                if(!parts.ContainsKey(index))
                    parts[index]=new OrderPart() {Name = index};

                return parts[index];
            }
            set { parts[index] = value; }
        }


        IEnumerator<OrderPart> IEnumerable<OrderPart>.GetEnumerator()
        {
            return parts.Values.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return parts.Values.GetEnumerator();
        }
    }
}