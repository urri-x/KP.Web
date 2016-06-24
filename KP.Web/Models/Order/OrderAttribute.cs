using System;

namespace KP.WebApi.Models.Order
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal class OrderAttribute : Attribute
    {

        public string Code{get; set; }
    }
}