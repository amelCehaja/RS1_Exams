using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class PrikazOdjeljenjaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string SkolskaGodina { get; set; }
            public int Razred { get; set; }
            public string Oznaka { get; set; }
            public string Razrdenik { get; set; }
            public string PrebaceniUVise { get; set; }
            public double? ProsjekOcjena { get; set; }
            public string NajboljiUcenik { get; set; }
        }
        public List<Row> Odjeljenja { get; set; }
    }
}
