using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class PrikazMaturskihIspita
    {
        public class Row
        {
            public string Datum { get; set; }
            public string Odjeljenje { get; set; }
            public string Ispitivac { get; set; }
            public float? ProsjecniBodovi { get; set; }
            public int ID { get; set; }
        }
        public List<Row> MaturskiRadovi { get; set; }
    }
}
