using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_09_11_DotnetCore.ViewModels
{
    public class OdjeljenjeUceniciVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public int BrojZakljucnih { get; set; }
            public int BrojUDnevniku { get; set; }
        }
        public List<Row> Ucenici { get; set; }
    }
}
