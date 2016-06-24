using System;

namespace KP.WebApi.Repository.Entity
{
    public class DynamicRekv
    {
        public int Id;
        public int ObjectId;
        public DateTime DateBegin;
        public dynamic Value;
        public int IdRekv = 0;
        public string AsString;
        public bool AsBool;
        public DateTime AsDate;
        public double AsDouble;
    }
}