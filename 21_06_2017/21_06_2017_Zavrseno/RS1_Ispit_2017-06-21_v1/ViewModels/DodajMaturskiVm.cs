using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_Ispit_2017_06_21_v1.ViewModels
{
    public class DodajMaturskiVm
    {
        public int NastavnikID { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }
        public DateTime Datum { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }
        public int OdjeljenjeID { get; set; }
    }
}
