using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_02_15.Helper;
using Ispit_2017_02_15.Models;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ispit_2017_02_15.Controllers
{
    public class CasController : Controller
    {
        private MojContext _db;
        public CasController(MojContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            Nastavnik nastavnik = Autentifikacija.GetLogiraniKorisnik(this.HttpContext);
            OdrzaniCasoviPrikaz model = new OdrzaniCasoviPrikaz();
            model.Nastavnik = nastavnik.Ime.Substring(0, 1) + ". " + nastavnik.Prezime;
            model.Casovi = _db.OdrzaniCasovi
                .Where(x => x.Angazovan.NastavnikId == nastavnik.Id)
                .Select(x => new OdrzaniCasoviPrikaz.Row
                {
                    ID = x.Id,
                    Datum = x.Datum.Date.ToString(),
                    AkademskaGodina = x.Angazovan.AkademskaGodina.Opis,
                    Predmet = x.Angazovan.Predmet.Naziv,
                    BrojPrisutnih = _db.OdrzaniCasDetalji
                        .Where(p => p.Prisutan == true && p.OdrzaniCasId == x.Id)
                        .Count().ToString() + " od " + _db.OdrzaniCasDetalji.Where(p => p.OdrzaniCasId == x.Id).Count().ToString(),
                    ProsjecnaOcjena = _db.SlusaPredmet
                        .Where(s => s.Angazovan.PredmetId == x.Angazovan.PredmetId && s.Ocjena != null)
                        .Select(s => s.Ocjena)
                        .Average()
                }).ToList();
            return View(model);
        }
        public IActionResult Dodaj()
        {
            Nastavnik nastavnik = Autentifikacija.GetLogiraniKorisnik(this.HttpContext);
            DodajCasVM model = new DodajCasVM
            {
                Nastavnik = nastavnik.Ime.Substring(0, 1) + ". " + nastavnik.Prezime,
                akGodPredmet = _db.Angazovan
                    .Where(x => x.NastavnikId == nastavnik.Id)
                    .Select(x => new SelectListItem
                    {
                        Text = x.AkademskaGodina.Opis + " / " + x.Predmet.Naziv,
                        Value = x.Id.ToString()
                    }).ToList()
            };
            return View(model);
        }
        public IActionResult SpremiCas(DodajCasVM model)
        {
            if (!ModelState.IsValid)
            {
                Nastavnik nastavnik = Autentifikacija.GetLogiraniKorisnik(this.HttpContext);
                model.akGodPredmet = _db.Angazovan
                    .Where(x => x.NastavnikId == nastavnik.Id)
                    .Select(x => new SelectListItem
                    {
                        Text = x.AkademskaGodina.Opis + " / " + x.Predmet.Naziv,
                        Value = x.Id.ToString()
                    }).ToList();
                return View("Edit", model);
            }
            OdrzaniCas odrzaniCas = new OdrzaniCas
            {
                AngazovanId = model.selectID,
                Datum = model.Datum
            };
            _db.Add(odrzaniCas);
            _db.SaveChanges();
            List<SlusaPredmet> slusaPredmets = _db.SlusaPredmet
                .Where(x => x.AngazovanId == model.selectID)
                .ToList();
            foreach(SlusaPredmet slusaPredmet in slusaPredmets)
            {
                OdrzaniCasDetalji detalji = new OdrzaniCasDetalji
                {
                    SlusaPredmetId = slusaPredmet.Id,
                    OdrzaniCasId = odrzaniCas.Id,
                    BodoviNaCasu = 0,
                    Prisutan = true
                };
                _db.Add(detalji);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Nastavnik nastavnik = Autentifikacija.GetLogiraniKorisnik(this.HttpContext);
            EditCasVM model = _db.OdrzaniCasovi
                .Where(x => x.Id == id)
                .Select(x => new EditCasVM
                {
                    ID = x.Id,
                    AkGod = x.Angazovan.AkademskaGodina.Opis + " / " + x.Angazovan.Predmet.Naziv,
                    Datum = x.Datum,
                    Nastavnik = nastavnik.Ime.Substring(0,1) + ". " + nastavnik.Prezime
                })
                .FirstOrDefault();
            return View(model);
        }
        public IActionResult SaveEdit(EditCasVM model)
        {
            OdrzaniCas odrzaniCas = _db.OdrzaniCasovi.Find(model.ID);
            odrzaniCas.Datum = model.Datum;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}