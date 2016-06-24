namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code= "ORD_SHTAT_CREATE")]
    public  class OrdShtatCreate 
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "������� �������";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ��������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������� ������������", true));
            return o;
        }
    }
}