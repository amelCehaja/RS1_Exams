using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;

namespace RS1_Ispit_2017_06_21_v1.Controllers
{

    public class IspitiController : Controller
    {
        private MojContext _db;
        public IspitiController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index()
        {
            PrikazMaturskihIspita model = new PrikazMaturskihIspita
            {
                MaturskiRadovi = _db.MaturskiIspit.Select(x => new PrikazMaturskihIspita.Row
                {
                    ID = x.ID,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Odjeljenje = x.Odjeljenje.Naziv,
                    Ispitivac = x.Ispitivac.ImePrezime,
                    ProsjecniBodovi = _db.MaturskiIspitStavka.Where(s => s.MaturskiIspitID == x.ID && s.Osloboden == false).Select(s => s.Bodovi).Average()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult DodajMaturski()
        {
            DodajMaturskiVm model = new DodajMaturskiVm
            {
                Nastavnici = _db.Nastavnik.Select(x => new SelectListItem
                {
                    Text = x.ImePrezime,
                    Value = x.Id.ToString()
                }).ToList(),
                Odjeljenja = _db.Odjeljenje.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }
        public IActionResult SpremiMaturski(DodajMaturskiVm model)
        {
            MaturskiIspit maturskiIspit = new MaturskiIspit
            {
                IspitivacID = model.NastavnikID,
                Datum = model.Datum,
                OdjeljenjeID = model.OdjeljenjeID
            };
            _db.Add(maturskiIspit);
            _db.SaveChanges();

            List<UpisUOdjeljenje> ucenici = _db.UpisUOdjeljenje.Where(x => x.OdjeljenjeId == model.OdjeljenjeID).ToList();
            foreach (var x in ucenici)
            {
                if (x.OpciUspjeh > 1 && _db.MaturskiIspitStavka.Where(m => m.UpisUOdjeljenjeID == x.Id && m.Bodovi > 50).Count() == 0)
                {
                    MaturskiIspitStavka maturskiIspitStavka = new MaturskiIspitStavka
                    {
                        MaturskiIspitID = maturskiIspit.ID,
                        UpisUOdjeljenjeID = x.Id,
                        Bodovi = 0,
                        Osloboden = false
                    };
                    if (x.OpciUspjeh == 5)
                    {
                        maturskiIspitStavka.Osloboden = true;
                        maturskiIspitStavka.Bodovi = null;
                    }
                    _db.Add(maturskiIspitStavka);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult DetaljiMaturskog(int maturskiID)
        {
            MaturskiIspit maturskiIspit = _db.MaturskiIspit.Include(x => x.Ispitivac).Include(x => x.Odjeljenje).Where(x => x.ID == maturskiID).SingleOrDefault();
            DetaljiMaturskogVM model = new DetaljiMaturskogVM
            {
                Nastavnik = maturskiIspit.Ispitivac.ImePrezime,
                Datum = maturskiIspit.Datum.ToString("dd.MM.yyyy"),
                ID = maturskiIspit.ID,
                Odjeljenje = maturskiIspit.Odjeljenje.Naziv
            };
            return View(model);
        }
    }
}