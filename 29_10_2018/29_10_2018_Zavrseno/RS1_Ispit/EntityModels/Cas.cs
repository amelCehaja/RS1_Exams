using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.EntityModels
{
    public class Cas
    {
        [Key]
        public int ID { get; set; }
        public int PredajePredmetID { get; set; }
        [ForeignKey("PredajePredmetID")]
        public PredajePredmet PredajePredmet { get; set; }
        public DateTime Datum { get; set; }
        public string SadrzajCasa { get; set; }
    }
}
