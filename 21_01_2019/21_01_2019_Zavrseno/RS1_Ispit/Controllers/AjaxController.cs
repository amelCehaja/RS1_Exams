using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class AjaxController : Controller
    {
        private MojContext _db;
        public AjaxController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index(int id)
        {
            PrikazUcenikaVm model = new PrikazUcenikaVm
            {
                Ucenici = _db.MaturskiIspitStavka.Where(x => x.MaturskiIspitID == id).Select(x => new PrikazUcenikaVm.Row
                {
                    ID = x.ID,
                    ImePrezime = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Pristupio = x.Pristupio == true ? "DA" : "NE",
                    Rezultat = x.Rezultat,
                    Prosjek = _db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavkaId == x.OdjeljenjeStavkaID).Select(p => p.ZakljucnoKrajGodine).Average()
                }).ToList()
            };
            return PartialView(model);
        }
        public IActionResult UcenikJePrisutan(int id)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(id);
            maturskiIspitStavka.Pristupio = true;
            maturskiIspitStavka.Rezultat = 0;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = maturskiIspitStavka.MaturskiIspitID });
        }
        public IActionResult UcenikJeOdsutan(int id)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(id);
            maturskiIspitStavka.Pristupio = false;
            maturskiIspitStavka.Rezultat = null;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = maturskiIspitStavka.MaturskiIspitID });
        }
        public IActionResult UrediRezultat(int id)
        {
            UrediRezultatVM model = _db.MaturskiIspitStavka.Where(x => x.ID == id).Select(x => new UrediRezultatVM
            {
                ID = x.ID,
                Rezultat = x.Rezultat,
                Ucenik = x.OdjeljenjeStavka.Ucenik.ImePrezime
            }).SingleOrDefault();
            return PartialView(model);
        }
        public IActionResult SpremiUrediRezultat(UrediRezultatVM model)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(model.ID);
            maturskiIspitStavka.Rezultat = model.Rezultat;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = maturskiIspitStavka.MaturskiIspitID });
        }
        public IActionResult SpremiRezultat(int id,int rezultat)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(id);
            maturskiIspitStavka.Rezultat = rezultat;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = maturskiIspitStavka.MaturskiIspitID });
        }
    }
}