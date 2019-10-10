using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            PrikazUcenikaVM model = new PrikazUcenikaVM
            {
                Ucenici = _db.CasStavka.Where(x => x.CasID == id).Select(x => new PrikazUcenikaVM.Row
                {
                    ID = x.ID,
                    ImePrezime = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                    Ocjena = x.Ocjena,
                    Prisutan = x.Prisutan == true ? "Prisutan" : "Odsutan",
                    Opravdano = x.OpravdanoOdsutan == true ? "DA" : x.OpravdanoOdsutan == false ? "NE" : null
                }).ToList()
            };
            return PartialView(model);
        }
        public IActionResult UcenikJePrisutan(int id)
        {
            CasStavka casStavka = _db.CasStavka.Find(id);
            casStavka.Prisutan = true;
            casStavka.OpravdanoOdsutan = null;
            casStavka.Napomena = null;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = casStavka.CasID });
        }
        public IActionResult UcenikJeOdsutan(int id)
        {
            CasStavka casStavka = _db.CasStavka.Find(id);
            casStavka.Prisutan = false;
            casStavka.OpravdanoOdsutan = false;
            casStavka.Ocjena = null;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = casStavka.CasID });
        }
        public IActionResult UrediUcenik(int id)
        {
            CasStavka casStavka = _db.CasStavka.Include(x => x.OdjeljenjeStavka.Ucenik).Where(x => x.ID == id).SingleOrDefault();
            if (casStavka.Prisutan == true)
            {
                UrediPrisutanVM model = new UrediPrisutanVM
                {
                    ID = casStavka.ID,
                    Ocjena = casStavka.Ocjena,
                    Ucenik = casStavka.OdjeljenjeStavka.Ucenik.ImePrezime
                };
                return PartialView("UrediPrisutan", model);
            }
            UrediOdsutanVM model1 = new UrediOdsutanVM
            {
                ID = casStavka.ID,
                Ucenik = casStavka.OdjeljenjeStavka.Ucenik.ImePrezime,
                Napomena = casStavka.Napomena,
                Opravdano = casStavka.OpravdanoOdsutan.GetValueOrDefault()
            };
            return PartialView("UrediOdsutan", model1);
        }
        public IActionResult SpremiPrisutan(UrediPrisutanVM model)
        {
            CasStavka casStavka = _db.CasStavka.Find(model.ID);
            casStavka.Ocjena = model.Ocjena;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = casStavka.CasID });
        }
        public IActionResult SpremiOdsutan(UrediOdsutanVM model)
        {
            CasStavka casStavka = _db.CasStavka.Find(model.ID);
            casStavka.Napomena = model.Napomena;
            casStavka.OpravdanoOdsutan = model.Opravdano;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = casStavka.CasID });
        }
    }
}