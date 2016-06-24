using System;
using System.Linq;
using System.Reflection;

namespace KP.WebApi.Models.Order
{
    public static class OrderExtensions
    {
        public static Order ApplyTemplate(this Order o)
        {
            var templateClass = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => Attribute.IsDefined((MemberInfo) t, typeof(OrderAttribute)))
                .FirstOrDefault(t => IsTemplateFor(t, o.Code));
            if (templateClass == null)
                return o;

            ConstructorInfo magicConstructor = templateClass.GetConstructor(Type.EmptyTypes);
            object magicClassObject = magicConstructor.Invoke(new object[] { });
            MethodInfo magicMethod = templateClass.GetMethod("ApplyTemplate");
            var result =  magicMethod.Invoke(magicClassObject, new object[] {o});
            return (Order)result;
        }

        private static bool IsTemplateFor(Type t, string Code)
        {
            var orderAttributes = (OrderAttribute[])Attribute.GetCustomAttributes(t, typeof(OrderAttribute));
            return orderAttributes
                .Select(x => x.Code)
                .Contains(Code);
        }
    }
}