using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017_06_21_v1.EF;
using RS1_Ispit_2017_06_21_v1.Models;
using RS1_Ispit_2017_06_21_v1.ViewModels;

namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class AjaxController : Controller
    {
        private MojContext _db;
        public AjaxController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult Index(int maturskiID)
        {
            PrikazUcenika model = new PrikazUcenika
            {
                Ucenici = _db.MaturskiIspitStavka.Where(x => x.MaturskiIspitID == maturskiID).Select(x => new PrikazUcenika.Row
                {
                    ID = x.ID,
                    ImePrezime = x.UpisUOdjeljenje.Ucenik.ImePrezime,
                    OpciUspjeh = x.UpisUOdjeljenje.OpciUspjeh,
                    Bodovi = x.Bodovi,
                    Osloboden = x.Osloboden == true ? "DA" : "NE"
                }).ToList()
            };
            return PartialView(model);
        }
        public IActionResult Uredi(int maturskiStavkaID)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Include(x => x.UpisUOdjeljenje.Ucenik).Where(x => x.ID == maturskiStavkaID).SingleOrDefault();
            UrediMaturskiStavkaVM model = new UrediMaturskiStavkaVM
            {
                ID = maturskiIspitStavka.ID,
                Bodovi = maturskiIspitStavka.Bodovi,
                ImePrezime = maturskiIspitStavka.UpisUOdjeljenje.Ucenik.ImePrezime
            };
            return PartialView(model);
        }
        public void Spremi(UrediMaturskiStavkaVM model)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(model.ID);
            maturskiIspitStavka.Bodovi = model.Bodovi;
            _db.SaveChanges();
        }
        public void Spremi2(int id,float bodovi)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(id);
            maturskiIspitStavka.Bodovi = bodovi;
            _db.SaveChanges();
        }
        public void PromijeniOsloboden(int id)
        {
            MaturskiIspitStavka maturskiIspitStavka = _db.MaturskiIspitStavka.Find(id);
            if(maturskiIspitStavka.Osloboden == true)
            {
                maturskiIspitStavka.Bodovi = 0;
                maturskiIspitStavka.Osloboden = false;
            }
            else
            {
                maturskiIspitStavka.Bodovi = null;
                maturskiIspitStavka.Osloboden = true;
            }
            _db.SaveChanges();
        }
    }
}