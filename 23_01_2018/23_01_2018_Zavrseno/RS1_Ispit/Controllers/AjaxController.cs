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
    public class AjaxController : Controller
    {
        private MojContext _db;
        public AjaxController(MojContext mojContext)
        {
            _db = mojContext;
        }
        public IActionResult RezultatiPretrage(int uputnicaID,int? rezultatID)
        {
            RezultatiPretrageVM model = this.rezultati(uputnicaID);
            if (rezultatID.HasValue)
            {
                RezultatiPretrageVM rezultati = Uredi(model, rezultatID);
                return PartialView(rezultati);
            }

            return PartialView(model);
        }
        public RezultatiPretrageVM rezultati(int id)
        {
            List<RezultatPretrage> rezultati = _db.RezultatPretrage.Include(x => x.LabPretraga).Include(x => x.Modalitet).Where(x => x.UputnicaId == id).ToList();
            RezultatiPretrageVM model = new RezultatiPretrageVM
            {
                Rezultati = new List<RezultatiPretrageVM.Row>(),
                IsGotov = _db.Uputnica.Find(id).IsGotovNalaz,
                uputnicaID = id
            };
            if (rezultati.Count == 0)
                return null;
            foreach (var x in rezultati)
            {
                model.Rezultati.Add(new RezultatiPretrageVM.Row
                {
                    ID = x.Id,
                    IzmjereneVrijednosti = x.NumerickaVrijednost.HasValue ? x.NumerickaVrijednost.ToString() : x.ModalitetId.HasValue ? x.Modalitet.Opis : "(nije evidentirano)",
                    Pretraga = x.LabPretraga.Naziv,
                    JMJ = x.LabPretraga.MjernaJedinica,                  
                });
                List<Modalitet> modaliteti = _db.Modalitet.Where(m => m.LabPretragaId == x.LabPretragaId).ToList();

                if (modaliteti.Count == 0)
                {
                    model.Rezultati.Last().ReferenteVrijedosti = x.LabPretraga.ReferentnaVrijednostMin.ToString() + " - " + x.LabPretraga.ReferentnaVrijednostMax.ToString();
                    if (x.NumerickaVrijednost != null && (x.NumerickaVrijednost < x.LabPretraga.ReferentnaVrijednostMin || x.NumerickaVrijednost > x.LabPretraga.ReferentnaVrijednostMax))
                        model.Rezultati.Last().IsReferentna = false;
                    else
                        model.Rezultati.Last().IsReferentna = true;
                }
                else
                {
                    model.Rezultati.Last().ReferenteVrijedosti = " ";
                    model.Rezultati.Last().IsReferentna = false;
                    foreach (var modalitet in modaliteti)
                    {
                        if (modalitet.IsReferentnaVrijednost == true)
                            model.Rezultati.Last().ReferenteVrijedosti += modalitet.Opis + ", ";
                        if (x.ModalitetId == modalitet.Id && modalitet.IsReferentnaVrijednost == true)
                            model.Rezultati.Last().IsReferentna = true;
                    }
                }
            }
            return model;
        }        
        public RezultatiPretrageVM Uredi(RezultatiPretrageVM model,int? id)
        {
            RezultatPretrage rezultatPretrage = _db.RezultatPretrage.Include(x => x.LabPretraga).Where(x => x.Id == id).SingleOrDefault();
            
            model.urediRezultat = new RezultatiPretrageVM.UrediRezultat
            {
                ID = rezultatPretrage.Id,
                Pretraga = rezultatPretrage.LabPretraga.Naziv,
                IzmjerenaVrijednost = rezultatPretrage.NumerickaVrijednost,
                VrijednostID = rezultatPretrage.ModalitetId
            };          
            
            model.urediRezultat.Vrijednost = _db.Modalitet.Where(x => x.LabPretragaId == rezultatPretrage.LabPretragaId).Select(x => new SelectListItem
            {
                Text = x.Opis,
                Value = x.Id.ToString()
            }).ToList();

            return model;
        }
        public void Spremi(RezultatiPretrageVM model)
        {
            RezultatPretrage rezultat = _db.RezultatPretrage.Find(model.urediRezultat.ID);
            if(model.urediRezultat.IzmjerenaVrijednost.HasValue)
                rezultat.NumerickaVrijednost = model.urediRezultat.IzmjerenaVrijednost;
            else
                rezultat.ModalitetId = model.urediRezultat.VrijednostID;
            _db.SaveChanges();
        }
    }
}