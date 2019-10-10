using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class CasStavka
    {
        [Key]
        public int ID { get; set; }
        public int CasID { get; set; }
        [ForeignKey("CasID")]
        public Cas Cas { get; set; }
        public int OdjeljenjeStavkaID { get; set; }
        [ForeignKey("OdjeljenjeStavkaID")]
        public OdjeljenjeStavka OdjeljenjeStavka { get; set; }
        public int? Ocjena { get; set; }
        public bool Prisutan { get; set; }
        public string Napomena { get; set; }
        public bool? OpravdanoOdsutan { get; set; }
    }
}
