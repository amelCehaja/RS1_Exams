using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit_2017_02_15.ViewModels
{
    public class PrikazUcenikaVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string ImePrezime { get; set; }
            public int Bodovi { get; set; }
            public bool Prisutan { get; set; }
        }
        public List<Row> ucenici { get; set; }
    }
}
