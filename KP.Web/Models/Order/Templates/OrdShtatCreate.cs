namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code= "ORD_SHTAT_CREATE")]
    public  class OrdShtatCreate 
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "Штатные единицы";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "Дата создания", true));
            fields.Add(new OrderField("object_code", "Код", true));
            fields.Add(new OrderField("shortname", "Краткое наименование", true));
            return o;
        }
    }
}