using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit.Data;
using Ispit.Data.EntityModels;
using Ispit.Web.Helper;
using Ispit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit.Web.Controllers
{
    [Autorizacija]
    public class AjaxController : Controller
    {
        private MyContext _db;
        public AjaxController(MyContext myContext)
        {
            _db = myContext;
        }
        public IActionResult Index(int oznacenDogadajID)
        {
            ObavezeDogadajaVM model = new ObavezeDogadajaVM
            {
                Obaveze = _db.StanjeObaveze.Where(x => x.OznacenDogadjajID == oznacenDogadajID).Select(x => new ObavezeDogadajaVM.Row
                {
                    ID = x.Id,
                    Naziv = x.Obaveza.Naziv,
                    BrojDana = x.NotifikacijaDanaPrije,
                    PonavljajNotifikaciju = x.NotifikacijeRekurizivno == true ? "DA" : "NE",
                    Procent = x.IzvrsenoProcentualno.ToString() + "%"
                }).ToList()
            };

            return PartialView(model);
        }
        public IActionResult Uredi(int stanjeObavezaID)
        {
            UrediStanjeObaveze model = _db.StanjeObaveze.Where(x => x.Id == stanjeObavezaID).Select(x => new UrediStanjeObaveze
            {
                ID = x.Id,
                Obaveza = x.Obaveza.Naziv,
                ZavrsenoProcentualno = x.IzvrsenoProcentualno
            }).SingleOrDefault();
            return PartialView(model);
        }
        public void Spremi(UrediStanjeObaveze model)
        {
            StanjeObaveze stanjeObaveze = _db.StanjeObaveze.Find(model.ID);
            stanjeObaveze.IzvrsenoProcentualno = model.ZavrsenoProcentualno;
            _db.SaveChanges();
        }
    }
}