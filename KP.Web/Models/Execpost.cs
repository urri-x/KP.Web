using System;
using KP.WebApi.Query;

namespace KP.WebApi.Models
{
    public class Execpost
    {
        public int Id;
        public int IdShtat;
        public int IdPersonal;
        public int IdWork;
        public DateTime DateBegin;
        public DateTime? DateEnd;
        public int IdTopPodr;
        public int IdFirm;
    }
    
}