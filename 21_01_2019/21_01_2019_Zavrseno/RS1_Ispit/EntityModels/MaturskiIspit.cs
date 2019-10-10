using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class MaturskiIspit
    {
        [Key]
        public int ID { get; set; }
        public int SkolaID { get; set; }
        [ForeignKey("SkolaID")]
        public Skola Skola { get; set; }
        public int NastavnikID { get; set; }
        [ForeignKey("NastavnikID")]
        public Nastavnik Nastavnik { get; set; }
        public int SkolskaGodinaID { get; set; }
        [ForeignKey("SkolskaGodinaID")]
        public SkolskaGodina SkolskaGodina { get; set; }
        public int PredmetID { get; set; }
        [ForeignKey("PredmetID")]
        public Predmet Predmet { get; set; }
        public DateTime DatumIspita { get; set; }
        public string Napomena { get; set; }
    }
}
