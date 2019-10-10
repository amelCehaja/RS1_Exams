using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class RezultatiPretrageVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Pretraga { get; set; }
            public string IzmjereneVrijednosti { get; set; }
            public string JMJ { get; set; }
            public string ReferenteVrijedosti { get; set; }
            public bool IsReferentna { get; set; }
        }
        public class UrediRezultat
        {
            public int ID { get; set; }
            public string Pretraga { get; set; }
            public double? IzmjerenaVrijednost { get; set; }
            public List<SelectListItem> Vrijednost { get; set; }
            public int? VrijednostID { get; set; }
        }
        public UrediRezultat urediRezultat { get; set; }
        public List<Row> Rezultati { get; set; }
        public bool IsGotov { get; set; }
        public int uputnicaID { get; set; }
    }
}
