using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            PrikazNastavnika model = new PrikazNastavnika
            {
                Nastavnici = _db.PredajePredmet.Where(x => x.Odjeljenje.SkolskaGodina.Aktuelna == true).Select(x => new PrikazNastavnika.Row
                {
                    NastavnikID = x.NastavnikID,
                    Nastavnik = x.Nastavnik.Ime + " " + x.Nastavnik.Prezime,
                    Skola = x.Odjeljenje.Skola.Naziv,
                    SkolaID = x.Odjeljenje.SkolaID
                }).Distinct().ToList()
            };
            return View(model);
        }
        public IActionResult OdabirNastavnika(int nastavnikID, int skolaID)
        {
            PrikazOdrzaniCasovaVM model = new PrikazOdrzaniCasovaVM
            {
                Casovi = _db.Cas.Where(x => x.PredajePredmet.NastavnikID == nastavnikID && x.PredajePredmet.Odjeljenje.SkolaID == skolaID).Select(x => new PrikazOdrzaniCasovaVM.Row
                {
                    ID = x.ID,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Predmet = x.PredajePredmet.Predmet.Naziv,
                    SkolaOdjeljenje = x.PredajePredmet.Odjeljenje.SkolskaGodina.Naziv + " " + x.PredajePredmet.Odjeljenje.Oznaka,
                    Odsutni = _db.CasStavka.Where(s => s.CasID == x.ID && s.Prisutan == false).Select(s => s.OdjeljenjeStavka.Ucenik.ImePrezime + " ").ToList()
                }).ToList(),
                NastavnikID = nastavnikID,
                SKolaID = skolaID
            };
            return View(model);
        }
        public IActionResult DodajCas(int nastavnikID, int skolaID)
        {
            DodajCasVM model = new DodajCasVM
            {
                NastavnikID = nastavnikID,
                Nastavnik = _db.Nastavnik.Where(x => x.Id == nastavnikID).Select(x => x.Ime + " " + x.Prezime).SingleOrDefault(),
                OdjeljenjePredmet = _db.PredajePredmet.Where(x => x.NastavnikID == nastavnikID && x.Odjeljenje.Skola.Id == skolaID && x.Odjeljenje.SkolskaGodina.Aktuelna == true).Select(x => new SelectListItem
                {
                    Text = x.Odjeljenje.Oznaka + " / " + x.Predmet.Naziv,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult SpremiCas(DodajCasVM model)
        {
            Cas cas = new Cas
            {
                Datum = model.Datum,
                PredajePredmetID = model.OdjeljenjePredmetID
            };
            _db.Add(cas);
            _db.SaveChanges();
            int OdjeljenjeID = _db.PredajePredmet.Where(x => x.Id == cas.PredajePredmetID).Select(x => x.OdjeljenjeID).FirstOrDefault();

            foreach(var x in _db.OdjeljenjeStavka.Where(o => o.OdjeljenjeId == OdjeljenjeID).ToList())
            {
                CasStavka casStavka = new CasStavka
                {
                    CasID = cas.ID,
                    Prisutan = true,
                    OdjeljenjeStavkaID = x.Id
                };
                _db.Add(casStavka);
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult EditCas(int id)
        {
            EditCasVM model = _db.Cas.Where(x => x.ID == id).Select(x => new EditCasVM
            {
                ID = x.ID,
                Datum = x.Datum.ToString("dd.MM.yyyy"),
                Odjeljenje = x.PredajePredmet.Odjeljenje.Oznaka + " " + x.PredajePredmet.Predmet.Naziv,
                Sadrzaj = x.SadrzajCasa
            }).SingleOrDefault();
            return View(model);
        }
        public IActionResult SpremiEdit(EditCasVM model)
        {
            Cas cas = _db.Cas.Find(model.ID);
            cas.SadrzajCasa = model.Sadrzaj;
            _db.SaveChanges();
            return RedirectToAction("EditCas", new { id = model.ID });
        }
    }
}