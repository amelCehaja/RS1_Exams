using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.Models
{
    public class MaturskiIspitStavka
    {
        [Key]
        public int ID { get; set; }
        public float? Bodovi { get; set; }
        public bool Osloboden { get; set; }
        public int MaturskiIspitID { get; set; }
        [ForeignKey("MaturskiIspitID")]
        public MaturskiIspit MaturskiIspit { get; set; }
        public int UpisUOdjeljenjeID { get; set; }
        [ForeignKey("UpisUOdjeljenjeID")]
        public UpisUOdjeljenje UpisUOdjeljenje { get; set; }
    }
}
