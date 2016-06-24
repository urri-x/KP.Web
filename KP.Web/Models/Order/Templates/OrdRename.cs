namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_RENAME")]
    public class OrdRename
    {
        public Order ApplyTemplate(Order o)
        {
            o.Parts["ResponsibleData"].Visible = false;
            o.Parts["ResponsiblePodrData"].Visible = false;

            o.Parts["PodrData"].Caption = "�������������";
            var fields = o.Parts["PodrData"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ���������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("oldshortname", "������ ������������", true));
            fields.Add(new OrderField("shortname", "����� ������������", true));

            o.Parts["Data"].Caption = "������ �������";
            fields = o.Parts["Data"].Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ���������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("oldshortname", "������ ������������", true));
            fields.Add(new OrderField("shortname", "����� ������������", true));

            return o;
        }
    }
}