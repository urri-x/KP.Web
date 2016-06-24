namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_KILL_POST")]
    public class OrdKillPost
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "Штатные единицы";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("kill_date", "Дата расформирования", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("post_name", "Краткое наименование", true));
            return o;
        }
    }
}