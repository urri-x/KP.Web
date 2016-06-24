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
            part.Caption = "�������������";
            var fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ���������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������������", true));
            fields.Add(new OrderField("parentobject_shortname", "������������ ������", true));

            part = o.Parts["Data"];
            part.Caption = "������ �������";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ���������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������������", true));
            fields.Add(new OrderField("parentobject_shortname", "������������ ������", true));
            return o;
        }
    }
}