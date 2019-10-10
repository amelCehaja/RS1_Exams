using Ispit_2017_09_11_DotnetCore.EF;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ispit_2017_09_11_DotnetCore.EntityModels;

namespace Ispit_2017_09_11_DotnetCore.Helpers
{
    public class ProsjekUcenika
    {
        public string ImePrezime { get; set; }
        public double Prosjek { get; set; }
    }
    public static class OdjeljenjeHelper
    {
        public static string NajboljiUcenik(this HttpContext httpContext,int odjeljenjeID)
        {
            MojContext _db = httpContext.RequestServices.GetService<MojContext>();
            List<Ucenik> ucenici = _db.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == odjeljenjeID).Select(x => x.Ucenik).ToList();
            List<ProsjekUcenika> prosjekUcenika = new List<ProsjekUcenika>();
            foreach(var ucenik in ucenici)
            {
                var prosjek = _db.DodjeljenPredmet.Where(x => x.OdjeljenjeStavka.UcenikId == ucenik.Id && x.OdjeljenjeStavka.OdjeljenjeId == odjeljenjeID && x.ZakljucnoKrajGodine == 1).Count() > 0 ? 0 : _db.DodjeljenPredmet.Where(x => x.OdjeljenjeStavka.UcenikId == ucenik.Id && x.ZakljucnoKrajGodine != null).Select(x => x.ZakljucnoKrajGodine).Average();
                prosjekUcenika.Add(new ProsjekUcenika
                {
                    ImePrezime = ucenik.ImePrezime,
                    Prosjek = prosjek
                });
            }         
            return prosjekUcenika.OrderByDescending(x => x.Prosjek).ElementAt(0).ImePrezime;
        }
    }
}
