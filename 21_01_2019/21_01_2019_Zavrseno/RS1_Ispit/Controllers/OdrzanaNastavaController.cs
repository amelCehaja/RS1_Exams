using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private MojContext _db;
        public OdrzanaNastavaController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index()
        {
            PrikazNastavnikaVM model = new PrikazNastavnikaVM
            {
                Nastavnici = _db.Nastavnik.Select(x => new PrikazNastavnikaVM.Row
                {
                    ID = x.Id,
                    ImePrezime = x.Ime + " " + x.Prezime,
                    Skola = _db.PredajePredmet.Where(p => p.NastavnikID == x.Id).Select(p => p.Odjeljenje.Skola.Naziv).Distinct().ToList()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult PrikazMaturskihIspita(int id)
        {
            PrikazMaturskihIspitaVM model = new PrikazMaturskihIspitaVM
            {
                MaturskiIspiti = _db.MaturskiIspit.Where(x => x.NastavnikID == id).Select(x => new PrikazMaturskihIspitaVM.Row
                {
                    ID = x.ID,
                    Datum = x.DatumIspita.ToString("dd.MM.yyyy"),
                    Predmet = x.Predmet.Naziv,
                    Skola = x.Skola.Naziv,
                    NisuPristupili = _db.MaturskiIspitStavka.Where(m => m.MaturskiIspitID == x.ID && m.Pristupio == false).Select(m => m.OdjeljenjeStavka.Ucenik.ImePrezime).ToList()
                }).ToList(),
                NastavnikID = id
            };
            return View(model);
        }
        public IActionResult DodajMaturski(int id)
        {
            DodajMaturskiVM model = new DodajMaturskiVM
            {
                NastavnikID = id,
                Nastavnik = _db.Nastavnik.Where(x => x.Id == id).Select(x => x.Ime + " " + x.Prezime).SingleOrDefault(),
                Predmeti = _db.PredajePredmet.Where(x => x.NastavnikID == id && x.Odjeljenje.Razred == 4).Select(x => new SelectListItem
                {
                    Text = x.Predmet.Naziv,
                    Value = x.Predmet.Id.ToString()
                }).Distinct().ToList(),
                SkolskaGodina = _db.SkolskaGodina.Where(x => x.Aktuelna == true).Select(x => x.Naziv).SingleOrDefault(),
                Skole = _db.PredajePredmet.Where(p => p.NastavnikID == id).Select(p => new SelectListItem
                {
                    Text = p.Odjeljenje.Skola.Naziv,
                    Value = p.Odjeljenje.Skola.Id.ToString()
                }).Distinct().ToList()
            };
            return View(model);
        }
        public IActionResult SpremiMaturskiIspit(DodajMaturskiVM model)
        {
            MaturskiIspit maturskiIspit = new MaturskiIspit
            {
                NastavnikID = model.NastavnikID,
                DatumIspita = model.DatumIspita,
                PredmetID = model.PredmetID,
                SkolaID = model.SkolaID,
                SkolskaGodinaID = _db.SkolskaGodina.Where(x => x.Aktuelna == true).Select(x => x.Id).SingleOrDefault()
            };
            _db.Add(maturskiIspit);
            _db.SaveChanges();

            foreach (var x in _db.OdjeljenjeStavka.Where(o => o.Odjeljenje.SkolskaGodina.Aktuelna == true && o.Odjeljenje.SkolaID == maturskiIspit.SkolaID && o.Odjeljenje.Razred == 4))
            {
                if (_db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavkaId == x.Id && p.ZakljucnoKrajGodine == 1).Count() == 0 && _db.MaturskiIspitStavka.Where(m => m.OdjeljenjeStavkaID == x.Id && m.Rezultat > 55).Count() == 0)
                {
                    MaturskiIspitStavka maturskiIspitStavka = new MaturskiIspitStavka
                    {
                        MaturskiIspitID = maturskiIspit.ID,
                        OdjeljenjeStavkaID = x.Id,
                        Pristupio = false,
                        Rezultat = null
                    };
                    _db.Add(maturskiIspitStavka);
                }
            }
            _db.SaveChanges();
            return RedirectToAction("UrediMaturskiIspit", new { id = maturskiIspit.ID });
        }
        public IActionResult UrediMaturskiIspit(int id)
        {
            UrediMaturskiVM model = _db.MaturskiIspit.Where(x => x.ID == id).Select(x => new UrediMaturskiVM
            {
                ID = x.ID,
                Datum = x.DatumIspita.ToString("dd.MM.yyyy"),
                Napomena = x.Napomena,
                Predmet = x.Predmet.Naziv
            }).SingleOrDefault();
            return View(model);
        }
        public IActionResult SpremiUredi(UrediMaturskiVM model)
        {
            MaturskiIspit maturskiIspit = _db.MaturskiIspit.Find(model.ID);
            maturskiIspit.Napomena = model.Napomena;
            _db.SaveChanges();
            return RedirectToAction("UrediMaturskiIspit", new { id = model.ID });
        }
    }
}