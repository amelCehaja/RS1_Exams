using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazNastavnikaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public List<string> Skola { get; set; }
            public string ImePrezime { get; set; }
        }
        public List<Row> Nastavnici { get; set; }
    }
}
