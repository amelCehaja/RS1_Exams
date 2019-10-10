using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazUcenikaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public int? Ocjena { get; set; }
            public string Prisutan { get; set; }
            public string Opravdano { get; set; }
        }
        public List<Row> Ucenici { get; set; }
    }
}
