using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class OdrzaniCasoviPrikaz
    {
        public class Row
        {
            public int ID { get; set; }
            public string Datum { get; set; }
            public string AkademskaGodina { get; set; }
            public string Predmet { get; set; }
            public string BrojPrisutnih { get; set; }
            public double? ProsjecnaOcjena { get; set; }
        }
        public string Nastavnik { get; set; }
        public List<Row> Casovi { get; set; }
    }
}
