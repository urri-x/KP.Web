namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_KILL_PODR")]
    public class OrdKillPodr
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "Подразделения";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("kill_date", "Дата расформирования", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("podr_name", "Краткое наименование", true));
            return o;
        }
    }
}