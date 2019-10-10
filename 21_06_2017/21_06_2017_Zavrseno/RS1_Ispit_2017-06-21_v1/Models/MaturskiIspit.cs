using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.Models
{
    public class MaturskiIspit
    {
        [Key]
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public int IspitivacID { get; set; }
        [ForeignKey("IspitivacID")]
        public Nastavnik Ispitivac { get; set; }
        public int OdjeljenjeID { get; set; }
        [ForeignKey("OdjeljenjeID")]
        public Odjeljenje Odjeljenje { get; set; }
    }
}
