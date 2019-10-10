using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class DodajUcenikaVM
    {
        public int OdjeljenjeID { get; set; }
        public int UcenikID { get; set; }
        public List<SelectListItem> Ucenici { get; set; }
        public int BrojUDnevniku { get; set; }
        public int? ID { get; set; }
        public string UcenikImePrezime { get; set; }
    }
}
