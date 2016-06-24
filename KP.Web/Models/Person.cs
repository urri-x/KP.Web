namespace KP.WebApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Tabn { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public  string Fio => Surname + ' ' + Name + ' ' + Patronymic;
    }
}