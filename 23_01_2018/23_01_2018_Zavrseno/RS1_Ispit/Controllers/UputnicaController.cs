using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ispit_2017_09_11_DotnetCore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1.Ispit.Web.Models;
using RS1_Ispit_asp.net_core.ViewModels;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class UputnicaController : Controller
    {
        private MojContext _db;
        public UputnicaController(MojContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            PrikazUputnicaVM model = new PrikazUputnicaVM
            {
                Uputnice = _db.Uputnica
                    .Select(u => new PrikazUputnicaVM.Row
                    {
                        ID = u.Id,
                        Uputio = u.DatumUputnice.ToShortDateString() + " | Dr" + u.UputioLjekar.Ime.Substring(0,1) + "." + u.UputioLjekar.Ime.Substring(u.UputioLjekar.Ime.IndexOf(' '),2),
                        VrstaPretrage = u.VrstaPretrage.Naziv,
                        Pacijent = u.Pacijent.Ime.Substring(0,1) + "." + u.Pacijent.Ime.Substring(u.Pacijent.Ime.IndexOf(' ')),
                        DatumEvidentiranja = u.DatumRezultata 
                    }).ToList()
            };
            return View(model);
        }

        public IActionResult Dodaj()
        {
            DodajUputnicuVM model = new DodajUputnicuVM
            {
                Ljekari = _db.Ljekar.Select(x => new SelectListItem
                {
                     Text = "Dr " + x.Ime.Substring(0,1) + "." + x.Ime.Substring(x.Ime.IndexOf(" "),2),
                     Value = x.Id.ToString()
                }).ToList(),
                Pacijenti = _db.Pacijent.Select(x => new SelectListItem
                {
                    Text = x.Ime.Substring(0,1) + x.Ime.Substring(x.Ime.IndexOf(" ")),
                    Value = x.Id.ToString()
                }).ToList(),
                VrstePretrage = _db.VrstaPretrage.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Spremi(DodajUputnicuVM model)
        {
            Uputnica uputnica = new Uputnica
            {
                DatumUputnice = model.Datum,
                IsGotovNalaz = false,
                UputioLjekarId = model.LjekarUputioID,
                PacijentId = model.PacijentID,
                VrstaPretrageId = model.VrstaPretrageID
            };
            _db.Add(uputnica);
            _db.SaveChanges();
            foreach (var x in _db.LabPretraga.Where(x => x.VrstaPretrageId == model.VrstaPretrageID))
            {
                RezultatPretrage rezultatPretrage = new RezultatPretrage
                {
                    LabPretragaId = x.Id,
                    UputnicaId = uputnica.Id
                };
                _db.Add(rezultatPretrage);
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Detalji(int id)
        {
            Uputnica uputnica = _db.Uputnica.Include(u => u.Pacijent).Where(u => u.Id == id).SingleOrDefault();
            DetaljiUputnicaVM model = new DetaljiUputnicaVM
            {
                ID = id,
                Pacijent = uputnica.Pacijent.Ime.Substring(0,1) + "." + uputnica.Pacijent.Ime.Substring(uputnica.Pacijent.Ime.IndexOf(" "),2),
                DatumUputnice = uputnica.DatumUputnice.ToString("dd.MM.yyyy"),
                DatumRezultata = uputnica.DatumRezultata.HasValue ? uputnica.DatumRezultata.ToString() : null
            };

            return View(model);
        }

        public IActionResult Zakljucaj(int id)
        {
            Uputnica uputnica = _db.Uputnica.Find(id);
            uputnica.IsGotovNalaz = true;
            uputnica.DatumRezultata = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("Detalji", new { id = id });
        }
    }
}