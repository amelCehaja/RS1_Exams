﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class DodajOdjeljenjeVM
    {
        public string SkolskaGodina { get; set; }
        public int Razred { get; set; }
        public string Oznaka { get; set; }
        public int RazrednikID { get; set; }
        public List<SelectListItem> Nastavnici { get; set; }
        public int? NizeOdjeljenjeId { get; set; }
        public List<SelectListItem> NizaOdjeljenja { get; set; }

    }
}
