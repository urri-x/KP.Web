using System;

namespace KP.WebApi.Models.Order
{
    public enum OrderFieldType
    {
        String, Date, Bool,Float, Int
    }

    public class OrderField
    {
        public string Name;
        private string caption;

        public string Caption
        {
            get { return String.IsNullOrEmpty(caption) ? Name :caption; }
            set { caption = value; }
        }

        public bool Visible;

        public OrderField(string name, string caption, bool visible)
        {
            Name = name;
            this.caption = caption;
            Visible = visible;
        }

        public OrderField()
        {
        }
    }

    public class OrderFieldValue
    {
        public string Name;
        public string AsString;
        public DateTime? AsDate;
        public bool? AsBool;
        public double? AsFloat;
        public int? AsInt;
        public dynamic Value
        {
            get
            {
                switch (Type)
                {
                    case OrderFieldType.Bool:
                        return AsBool ?? false ? "ДА" : "НЕТ";
                    case OrderFieldType.Date:
                        return AsDate.Value.ToShortDateString();
                    case OrderFieldType.Float:
                        return AsFloat;
                    case OrderFieldType.Int:
                        return AsInt;
                    default:
                        return AsString;
                }
            }
        }
        public OrderFieldType Type;
    }
}