using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class DodajCasVM
    {
        public string Nastavnik { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public List<SelectListItem> akGodPredmet { get; set; }
        public int selectID { get; set; }
    }
}
