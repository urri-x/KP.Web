namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_TOTAL_REORGANIZATION")]
    [Order(Code = "ORD_FIRM_RENAME")]
    public class OrdTotalReorganization
    {
        public Order ApplyTemplate(Order o)
        {
            var part = o.Parts["Data"];
            part.Caption = "Создаваемые штатные единицы";
            var fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата создания", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Краткое наименование", true));
            fields.Add(new OrderField("parentobject_shortname", "Родительский объект", true));

            part = o.Parts["ChangedData"];
            part.Caption = "Изменяемые штатные единицы";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата изменения", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("oldshortname", "Старое наименование", true));
            fields.Add(new OrderField("oldparentobject_shortname", "Старый родительский объект", true));
            fields.Add(new OrderField("shortname", "Новое наименование", true));
            fields.Add(new OrderField("parentobject_shortname", "Новый родительский объект", true));

            part = o.Parts["PodrData"];
            part.Caption = "Создаваемые подразделения";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата создания", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Краткое наименование", true));
            fields.Add(new OrderField("parentobject_shortname", "Родительский объект", true));

            return o;
        }
    }
}