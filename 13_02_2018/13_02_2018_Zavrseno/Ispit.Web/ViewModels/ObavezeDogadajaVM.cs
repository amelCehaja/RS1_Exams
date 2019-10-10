using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class ObavezeDogadajaVM
    {
        public class Row
        {
            public string Naziv { get; set; }
            public string Procent { get; set; }
            public int BrojDana { get; set; }
            public string PonavljajNotifikaciju { get; set; }
            public int ID { get; set; }
        }
        public List<Row> Obaveze { get; set; }
    }
}
