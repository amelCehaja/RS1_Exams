using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.Web.Helper;
using Ispit.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Ispit.Data;
using Ispit.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Ispit.Web.ViewModels;

namespace Ispit.Web.Controllers
{
    [Autorizacija]
    public class OznaceniDogadajiController : Controller
    {
        private MyContext _db;
        public OznaceniDogadajiController(MyContext myContext)
        {
            _db = myContext;
        }
        public IActionResult Index()
        {
            int studentID = HttpContext.GetLogiraniKorisnik().Id;
            PrikazDogadajaVM model = new PrikazDogadajaVM
            {
                neoznaceniDogadaji = _db.Dogadjaj.Select(x => new PrikazDogadajaVM.NeoznaceniDogadaj
                {
                    ID = x.ID,
                    Datum = x.DatumOdrzavanja,
                    Opis = x.Opis,
                    Nastavnik = x.Nastavnik.ImePrezime,
                    Oznacen = _db.OznacenDogadjaj.Where(o => o.DogadjajID == x.ID).Count() != 0 ? true : false,
                    BrojObaveza = _db.Obaveza.Where(o => o.DogadjajID == x.ID).Count()
                }).ToList()
            };
            model.oznaceniDogadaji = new List<PrikazDogadajaVM.OznaceniDogadaj>();
            foreach (var x in model.neoznaceniDogadaji.ToList())
            {
                if (x.Oznacen == true && _db.OznacenDogadjaj.Where(s => s.StudentID == studentID && s.DogadjajID == x.ID).Count()!=0)
                {
                    model.oznaceniDogadaji.Add(new PrikazDogadajaVM.OznaceniDogadaj
                    {
                        ID = _db.OznacenDogadjaj.Where(o => o.DogadjajID == x.ID).Select(o => o.ID).SingleOrDefault(),
                        Opis = x.Opis,
                        Datum = x.Datum,
                        Nastavnik = x.Nastavnik,
                        RealizovanoObaveza = Math.Round(_db.StanjeObaveze.Where(o => o.OznacenDogadjajID == _db.OznacenDogadjaj.Where(y => y.DogadjajID == x.ID).Select(y => y.ID).SingleOrDefault()).Select(o => o.IzvrsenoProcentualno).Average(), 2).ToString() + "%"
                    });
                    model.neoznaceniDogadaji.Remove(x);
                }
            }
            return View(model);
        }
        public IActionResult Dodaj(int id)
        {
            OznacenDogadjaj oznacenDogadjaj = new OznacenDogadjaj
            {
                DatumDodavanja = DateTime.Now,
                DogadjajID = id,
                StudentID = HttpContext.GetLogiraniKorisnik().Id
            };
            _db.Add(oznacenDogadjaj);
            _db.SaveChanges();
            foreach (Obaveza x in _db.Obaveza.Where(o => o.DogadjajID == id).ToList())
            {
                StanjeObaveze stanje = new StanjeObaveze
                {
                    IsZavrseno = false,
                    IzvrsenoProcentualno = 0,
                    ObavezaID = x.ID,
                    OznacenDogadjajID = oznacenDogadjaj.ID,
                    NotifikacijaDanaPrije = x.NotifikacijaDanaPrijeDefault,
                    NotifikacijeRekurizivno = x.NotifikacijeRekurizivnoDefault
                };
                _db.Add(stanje);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detalji(int id)
        {
            OznacenDogadjaj dogadjaj = _db.OznacenDogadjaj.Include(x => x.Dogadjaj).Include(x => x.Dogadjaj.Nastavnik).Where(x => x.ID == id).SingleOrDefault();
            DetaljiDogadaja model = new DetaljiDogadaja
            {
                ID = dogadjaj.ID,
                DatumDodavanja = dogadjaj.DatumDodavanja.ToString("dd.MM.yyyy"),
                DatumDogadaja = dogadjaj.Dogadjaj.DatumOdrzavanja.ToString("dd.MM.yyyy"),
                Nastavnik = dogadjaj.Dogadjaj.Nastavnik.ImePrezime.Substring(0, 1) + "." + dogadjaj.Dogadjaj.Nastavnik.ImePrezime.Substring(dogadjaj.Dogadjaj.Nastavnik.ImePrezime.IndexOf(" ")),
                Opis = dogadjaj.Dogadjaj.Opis
            };
            return View(model);
        }
    }
}