using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspitStavka
    {
        [Key]
        public int ID { get; set; }
        public int OdjeljenjeStavkaID { get; set; }
        [ForeignKey("OdjeljenjeStavkaID")]
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int MaturskiIspitID { get; set; }
        [ForeignKey("MaturskiIspitID")]
        public MaturskiIspit MaturskiIspit { get; set; }
        public int? Rezultat { get; set; }
        public bool Pristupio { get; set; }
    }
}
