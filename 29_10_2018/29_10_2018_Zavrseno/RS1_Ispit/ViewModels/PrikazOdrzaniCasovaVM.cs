using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazOdrzaniCasovaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Datum { get; set; }
            public string SkolaOdjeljenje { get; set; }
            public string Predmet { get; set; }
            public List<string> Odsutni { get; set; }
        }
        public List<Row> Casovi { get; set; }
        public int NastavnikID { get; set; }
        public int SKolaID { get; set; }
    }
}
