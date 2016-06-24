namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_KILL_PODR")]
    public class OrdKillPodr
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["Data"].Caption = "�������������";
            var fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("kill_date", "���� ���������������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("podr_name", "������� ������������", true));
            return o;
        }
    }
}