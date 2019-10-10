using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ispit.Web.ViewModels
{
    public class PrikazDogadajaVM
    {
        public class Dogadaj
        {
            public int ID { get; set; }
            public DateTime Datum { get; set; }
            public string Nastavnik { get; set; }
            public string Opis { get; set; }
            public bool Oznacen { get; set; }
        }
        public class NeoznaceniDogadaj : Dogadaj
        {
            public int BrojObaveza { get; set; }
        }
        public class OznaceniDogadaj : Dogadaj
        {
            public string RealizovanoObaveza { get; set; }
        }
        public List<NeoznaceniDogadaj> neoznaceniDogadaji { get; set; }
        public List<OznaceniDogadaj> oznaceniDogadaji { get; set; }
    }
}
