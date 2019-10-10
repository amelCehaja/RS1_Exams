using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazMaturskihIspitaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Datum { get; set; }
            public string Skola { get; set; }
            public string Predmet { get; set; }
            public List<String> NisuPristupili { get; set; }
        }
        public List<Row> MaturskiIspiti { get; set; }
        public int NastavnikID { get; set; }
    }
}
