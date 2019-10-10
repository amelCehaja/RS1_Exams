using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ispit_2017_09_11_DotnetCore.Helpers;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class OdjeljenjaController : Controller
    {
        private MojContext _db;
        public OdjeljenjaController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index()
        {
            PrikazOdjeljenjaVM model = new PrikazOdjeljenjaVM { Odjeljenja = new List<PrikazOdjeljenjaVM.Row>() };
            model.Odjeljenja = _db.Odjeljenje.Select(x => new PrikazOdjeljenjaVM.Row
            {
                ID = x.Id,
                SkolskaGodina = x.SkolskaGodina,
                Razred = x.Razred,
                Oznaka = x.Oznaka,
                Razrdenik = x.Nastavnik.ImePrezime,
                PrebaceniUVise = x.IsPrebacenuViseOdjeljenje == true ? "DA" : "NE",
                ProsjekOcjena = Math.Round(_db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavka.OdjeljenjeId == x.Id && p.ZakljucnoKrajGodine != null && p.ZakljucnoKrajGodine != 0).Count() > 0 ? _db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavka.OdjeljenjeId == x.Id && p.ZakljucnoKrajGodine > 0).Select(p => p.ZakljucnoKrajGodine).Average() : 0, 2),              
            }).ToList();

            foreach(var odjeljenje in model.Odjeljenja)
            {
                odjeljenje.NajboljiUcenik = HttpContext.NajboljiUcenik(odjeljenje.ID);
            }

            return View(model);
        }
        public IActionResult Dodaj()
        {
            DodajOdjeljenjeVM model = new DodajOdjeljenjeVM
            {
                Nastavnici = _db.Nastavnik.Select(x => new SelectListItem
                {
                    Text = x.ImePrezime,
                    Value = x.NastavnikID.ToString()
                }).ToList(),
                NizaOdjeljenja = _db.Odjeljenje.Where(x => x.IsPrebacenuViseOdjeljenje == false).Select(x => new SelectListItem
                {
                    Text = x.SkolskaGodina + ", " + x.Oznaka,
                    Value = x.Id.ToString()
                }).ToList()
            };
            model.NizaOdjeljenja.Insert(0, new SelectListItem
            {
                Text = "-------",
                Value = null
            });
            return View(model);
        }
        public IActionResult Spremi(DodajOdjeljenjeVM model)
        {
            Odjeljenje odjeljenje = new Odjeljenje
            {
                NastavnikID = model.RazrednikID,
                SkolskaGodina = model.SkolskaGodina,
                IsPrebacenuViseOdjeljenje = false,
                Oznaka = model.Oznaka,
                Razred = model.Razred
            };
            _db.Add(odjeljenje);
            _db.SaveChanges();
            List<Predmet> predmeti = _db.Predmet.Where(x => x.Razred == model.Razred).ToList();
            if (model.NizeOdjeljenjeId.HasValue)
            {
                Odjeljenje nizeOdjeljenje = _db.Odjeljenje.Find(model.NizeOdjeljenjeId);
                nizeOdjeljenje.IsPrebacenuViseOdjeljenje = true;
                List<OdjeljenjeStavka> odjeljenjeStavka = _db.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == model.NizeOdjeljenjeId).ToList();
                foreach (var x in odjeljenjeStavka)
                {
                    bool opciUspjeh = _db.DodjeljenPredmet.Where(d => d.OdjeljenjeStavkaId == x.Id && d.ZakljucnoKrajGodine == 1).Count() > 0 ? true : false;
                    if (opciUspjeh == false)
                    {
                        OdjeljenjeStavka stavka = new OdjeljenjeStavka
                        {
                            BrojUDnevniku = 0,
                            OdjeljenjeId = odjeljenje.Id,
                            UcenikId = x.UcenikId
                        };
                        _db.Add(stavka);
                        _db.SaveChanges();

                        foreach (var predmet in predmeti)
                        {
                            DodjeljenPredmet dodjeljenPredmet = new DodjeljenPredmet
                            {
                                OdjeljenjeStavkaId = stavka.Id,
                                PredmetId = predmet.Id,
                                ZakljucnoKrajGodine = 0,
                                ZakljucnoPolugodiste = 0
                            };
                            _db.Add(dodjeljenPredmet);
                            _db.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Obrisi(int odjeljenjeID)
        {
            List<OdjeljenjeStavka> odjeljenjeStavka = _db.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == odjeljenjeID).ToList();
            foreach (var x in odjeljenjeStavka)
            {
                List<DodjeljenPredmet> dodjeljenPredmet = _db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavkaId == x.Id).ToList();
                foreach (var y in dodjeljenPredmet)
                {
                    _db.Remove(y);
                }
                _db.Remove(x);
            }
            Odjeljenje odjeljenje = _db.Odjeljenje.Find(odjeljenjeID);
            _db.Remove(odjeljenje);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detalji(int odjeljenjeID)
        {
            Odjeljenje odjeljenje = _db.Odjeljenje.Include(x => x.Nastavnik).Where(x => x.Id == odjeljenjeID).SingleOrDefault();
            OdjeljenjeDetaljiVM model = new OdjeljenjeDetaljiVM
            {
                ID = odjeljenje.Id,
                Oznaka = odjeljenje.Oznaka,
                Razred = odjeljenje.Razred,
                SkolskaGodina = odjeljenje.SkolskaGodina,
                Razrednik = odjeljenje.NastavnikID.HasValue ? odjeljenje.Nastavnik.ImePrezime : "  ",
                BrojPredmeta = _db.Predmet.Where(x => x.Razred == odjeljenje.Razred).Count()
            };
            return View(model);
        }
        public IActionResult RekonstrukcijaRednihBrojeva(int odjeljenjeID)
        {
            List<OdjeljenjeStavka> ucenici = _db.OdjeljenjeStavka.OrderBy(x => x.Ucenik.ImePrezime.Substring(x.Ucenik.ImePrezime.IndexOf(" ") + 1)).ThenBy(x => x.Ucenik.ImePrezime.Substring(0, x.Ucenik.ImePrezime.IndexOf(" "))).Where(x => x.OdjeljenjeId == odjeljenjeID).ToList();
            foreach (var x in ucenici)
            {
                x.BrojUDnevniku = ucenici.IndexOf(x) + 1;
            }
            _db.SaveChanges();

            return RedirectToAction("Detalji", new { odjeljenjeID = odjeljenjeID });
        }
    }
}