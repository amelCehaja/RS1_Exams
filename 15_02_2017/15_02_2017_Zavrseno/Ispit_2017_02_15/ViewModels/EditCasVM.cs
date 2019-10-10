using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class EditCasVM
    {
        public int ID { get; set; }
        public string AkGod { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public string Nastavnik { get; set; }
    }
}
