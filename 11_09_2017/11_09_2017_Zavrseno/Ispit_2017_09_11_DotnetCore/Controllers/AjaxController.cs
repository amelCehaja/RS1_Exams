using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Ispit_2017_09_11_DotnetCore.EntityModels;
using Ispit_2017_09_11_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ispit_2017_09_11_DotnetCore.Controllers
{
    public class AjaxController : Controller
    {
        private MojContext _db;
        public AjaxController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index(int odjeljenjeID)
        {
            OdjeljenjeUceniciVM model = new OdjeljenjeUceniciVM
            {
                Ucenici = _db.OdjeljenjeStavka.Where(x => x.OdjeljenjeId == odjeljenjeID).OrderBy(x => x.BrojUDnevniku).Select(x => new OdjeljenjeUceniciVM.Row
                {
                    ID = x.Id,
                    ImePrezime = x.Ucenik.ImePrezime,
                    BrojZakljucnih = _db.DodjeljenPredmet.Where(p => p.OdjeljenjeStavkaId == x.Id && p.ZakljucnoKrajGodine != null && p.ZakljucnoKrajGodine != 0).Count(),
                    BrojUDnevniku = x.BrojUDnevniku
                }).ToList()
            };

            return PartialView(model);
        }
        public IActionResult DodajUcenika(int odjeljenjeID)
        {
            DodajUcenikaVM model = new DodajUcenikaVM
            {
                Ucenici = _db.Ucenik.Select(x => new SelectListItem
                {
                    Text = x.ImePrezime,
                    Value = x.Id.ToString()
                }).ToList(),
                OdjeljenjeID = odjeljenjeID
            };

            return PartialView(model);
        }
        public void SpremiUcenika(DodajUcenikaVM model)
        {
            OdjeljenjeStavka odjeljenjeStavka;
            if (model.ID.HasValue)
            {
                 odjeljenjeStavka = _db.OdjeljenjeStavka.Find(model.ID);
            }
            else
            {
                 odjeljenjeStavka = new OdjeljenjeStavka();
                _db.Add(odjeljenjeStavka);
                odjeljenjeStavka.UcenikId = model.UcenikID;
            }
            odjeljenjeStavka.OdjeljenjeId = model.OdjeljenjeID;
            odjeljenjeStavka.BrojUDnevniku = model.BrojUDnevniku;         
            _db.SaveChanges();
        }
        public void ObrisiOdjeljenjeStavka(int odjeljenjeStavkaID)
        {
            OdjeljenjeStavka odjeljenjeStavka = _db.OdjeljenjeStavka.Find(odjeljenjeStavkaID);
            _db.Remove(odjeljenjeStavka);
            _db.SaveChanges();
        }
        public IActionResult UrediOdjeljenjeStavka(int odjeljenjeStavkaID)
        {
            DodajUcenikaVM model = _db.OdjeljenjeStavka.Where(x => x.Id == odjeljenjeStavkaID).Select(x => new DodajUcenikaVM
            {
                ID = x.Id,
                UcenikImePrezime = x.Ucenik.ImePrezime,
                UcenikID = x.UcenikId,
                OdjeljenjeID = x.OdjeljenjeId,
                BrojUDnevniku = x.BrojUDnevniku,
                Ucenici = _db.Ucenik.Select(y => new SelectListItem
                {
                    Text = y.ImePrezime,
                    Value = y.Id.ToString()
                }).ToList()
            }).SingleOrDefault();
            return PartialView("DodajUcenika",model);
        }
    }
}