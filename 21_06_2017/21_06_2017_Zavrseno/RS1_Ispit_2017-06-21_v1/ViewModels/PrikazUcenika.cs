using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class PrikazUcenika
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public int OpciUspjeh { get; set; }
            public float? Bodovi { get; set; }
            public string Osloboden { get; set; }
        }
        public List<Row> Ucenici { get; set; }
    }
}
