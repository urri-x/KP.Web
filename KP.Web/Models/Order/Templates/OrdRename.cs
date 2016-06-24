namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_RENAME")]
    public class OrdRename
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["ResponsibleData"].Visible = false;
            o.Parts["ResponsiblePodrData"].Visible = false;

            o.Parts["PodrData"].Caption = "Подразделения";
            var fields = o.Parts["PodrData"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата изменения", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("oldshortname", "Старое наименование", true));
            fields.Add(new OrderField("shortname", "Новое наименование", true));

            o.Parts["Data"].Caption = "Штаные единицы";
            fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата изменения", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("oldshortname", "Старое наименование", true));
            fields.Add(new OrderField("shortname", "Новое наименование", true));

            return o;
        }
    }
}