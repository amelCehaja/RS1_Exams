using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class DetaljiUputnicaVM
    {
        public int ID { get; set; }
        public string Pacijent { get; set; }
        public string DatumRezultata { get; set; }
        public string DatumUputnice { get; set; }
    }
}
