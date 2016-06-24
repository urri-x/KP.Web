namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_PODR_CREATE")]
    public class OrdPodrCreate
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "�������������";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("QDate", "���� ��������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������� ������������", true));
            return o;
        }
    }
}