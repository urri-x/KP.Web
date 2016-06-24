namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_HISTORY")]
    [Order(Code = "65")]
    public class OrdHistory
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Transfer"].Visible = false;
            o.Parts["ObjectsIDs"].Visible = false;

            var part = o.Parts["PodrData"];
            part.Caption = "Подразделения";
            var fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата изменения", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Наименование", true));
            fields.Add(new OrderField("parentobject_shortname", "Родительский объект", true));

            part = o.Parts["Data"];
            part.Caption = "Штаные единицы";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата изменения", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Наименование", true));
            fields.Add(new OrderField("parentobject_shortname", "Родительский объект", true));
            return o;
        }
    }
}