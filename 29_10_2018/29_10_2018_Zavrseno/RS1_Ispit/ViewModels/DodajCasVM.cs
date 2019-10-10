using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_asp.net_core.ViewModels
{
    public class DodajCasVM
    {
        public int NastavnikID { get; set; }
        public string Nastavnik { get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> OdjeljenjePredmet { get; set; }
        public int OdjeljenjePredmetID { get; set; }
    }
}
