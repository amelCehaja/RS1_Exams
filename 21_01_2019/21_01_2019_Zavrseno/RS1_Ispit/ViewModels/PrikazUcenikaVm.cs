using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazUcenikaVm
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public double Prosjek { get; set; }
            public string Pristupio { get; set; }
            public int? Rezultat { get; set; }
        }
        public List<Row> Ucenici { get; set; }
    }
}
