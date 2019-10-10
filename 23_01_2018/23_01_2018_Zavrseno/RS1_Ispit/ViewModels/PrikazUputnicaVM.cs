using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class PrikazUputnicaVM
    {
        public class Row
        {
            public string Uputio { get; set; }
            public string Pacijent { get; set; }
            public string VrstaPretrage { get; set; }
            public DateTime? DatumEvidentiranja { get; set; }
            public int ID { get; set; }
        }
        public List<Row> Uputnice { get; set; }
    }
}
