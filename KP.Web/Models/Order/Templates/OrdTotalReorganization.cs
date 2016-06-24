namespace KP.WebApi.Models.Order.Templates
{
    [Order(Code = "ORD_TOTAL_REORGANIZATION")]
    [Order(Code = "ORD_FIRM_RENAME")]
    public class OrdTotalReorganization
    {
        public Order ApplyTemplate(Order o)
        {
            var part = o.Parts["Data"];
            part.Caption = "����������� ������� �������";
            var fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ��������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������� ������������", true));
            fields.Add(new OrderField("parentobject_shortname", "������������ ������", true));

            part = o.Parts["ChangedData"];
            part.Caption = "���������� ������� �������";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ���������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("oldshortname", "������ ������������", true));
            fields.Add(new OrderField("oldparentobject_shortname", "������ ������������ ������", true));
            fields.Add(new OrderField("shortname", "����� ������������", true));
            fields.Add(new OrderField("parentobject_shortname", "����� ������������ ������", true));

            part = o.Parts["PodrData"];
            part.Caption = "����������� �������������";
            fields = part.Fields;
            fields.Clear();
            fields.Add(new OrderField("date_begin", "���� ��������", true));
            fields.Add(new OrderField("object_code", "���", true));
            fields.Add(new OrderField("shortname", "������� ������������", true));
            fields.Add(new OrderField("parentobject_shortname", "������������ ������", true));

            return o;
        }
    }
}