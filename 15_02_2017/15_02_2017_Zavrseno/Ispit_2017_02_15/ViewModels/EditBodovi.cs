using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class EditBodovi
    {
        public int ID { get; set; }
        public string Student { get; set; }
        [Required]
        [Range(0,100)]
        public int Bodovi { get; set; }
    }
}
