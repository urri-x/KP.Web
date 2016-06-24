namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_PODR_CREATE")]
    public class OrdPodrCreate
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "Подразделения";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("QDate", "Дата создания", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Краткое наименование", true));
            return o;
        }
    }
}