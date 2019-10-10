using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_02_15.EF;
using Ispit_2017_02_15.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Ispit_2017_02_15.Models;

namespace Ispit_2017_02_15.Controllers
{
    public class AjaxController : Controller
    {
        private MojContext _db;
        public AjaxController(MojContext mojContext)
        {
            _db = mojContext;
        }
        
        public IActionResult PrikazUcenika(int id)
        {
            PrikazUcenikaVM model = new PrikazUcenikaVM();
            model.ucenici = _db.OdrzaniCasDetalji
                .Where(x => x.OdrzaniCasId == id)
                .Select(x => new PrikazUcenikaVM.Row
                {
                    ID = x.Id,
                    ImePrezime = x.SlusaPredmet.UpisGodine.Student.Ime + " " + x.SlusaPredmet.UpisGodine.Student.Prezime,
                    Bodovi = x.BodoviNaCasu,
                    Prisutan = x.Prisutan
                }).ToList();
            return PartialView(model);
        }

        public void UcenikJeOdsutan(int id)
        {
            OdrzaniCasDetalji detalji = _db.OdrzaniCasDetalji.Find(id);
            detalji.Prisutan = false;
            detalji.BodoviNaCasu = 0;
            _db.SaveChanges();
        }

        public void UcenikJePrisutan(int id)
        {
            OdrzaniCasDetalji detalji = _db.OdrzaniCasDetalji.Find(id);
            detalji.Prisutan = true;
            _db.SaveChanges();
        }
        public IActionResult Uredi(int id)
        {
            OdrzaniCasDetalji detalji = _db.OdrzaniCasDetalji.Find(id);
            EditBodovi model = new EditBodovi
            {
                ID = detalji.Id,
                Student = _db.OdrzaniCasDetalji.Where(x=> x.Id == id).Select(x=> x.SlusaPredmet.UpisGodine.Student.Ime + " " + x.SlusaPredmet.UpisGodine.Student.Prezime).FirstOrDefault(),
                Bodovi = detalji.BodoviNaCasu
            };
            return PartialView(model);
        }
        public IActionResult SaveEdit(EditBodovi model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Uredi", model);
            }
            OdrzaniCasDetalji detalji = _db.OdrzaniCasDetalji.Find(model.ID);
            detalji.BodoviNaCasu = model.Bodovi;
            _db.SaveChanges();

            return RedirectToAction("PrikazUcenika",new { id = detalji.OdrzaniCasId});
        }

    }
}