using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModel;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class TakmicenjeController : Controller
    {
        private readonly MojContext _db;
        public TakmicenjeController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Korak1 ViewModel = new Korak1
            {
                Skole = _db.Skola.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View("Korak1",ViewModel);
        }
        public IActionResult Korak2(Korak1 model)
        {
            Korak2 ViewModel = new Korak2
            {
                SkolaDomacin = _db.Skola.Find(model.SkolaID).Naziv,
                SkolaDomacinId= _db.Skola.Find(model.SkolaID).Id,
            };

            ViewModel.takmicenja = _db.Takmicenje.Where(x => x.SkolaId == model.SkolaID).Select(x => new Korak2.Takmicenja
            {
                Razred = x.Razred,
                Datum = x.Datum.ToShortDateString(),
                Predmet = x.Predmet.Naziv,
                TakmicenjeID = x.Id,
                BrojUcenikaKojiNisuPrisupili = _db.TakmicenjeUcesnik.Where(y => y.TakmicenjeId == x.Id).Count(y => !y.Pristupio),
                NajboljiUcesnik = _db.TakmicenjeUcesnik.Where(y => y.TakmicenjeId == x.Id && y.Bodovi>0).OrderByDescending(y => y.Bodovi).Select(y => y.OdjeljenjeStavka.Ucenik.ImePrezime).FirstOrDefault()
            });
            return View(ViewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 ViewModel = new Korak3
            {
                SkolaDomacin = _db.Skola.Find(Id).Naziv,
                Predmet = _db.Predmet.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv+x.Razred
                }),
                Datum = DateTime.Now,
                SkolaDomacinId = Id
            };
            return View(ViewModel);
        }
        public IActionResult Korak3Snimi(Korak3 model)
        {
            var _takmicenje= new Takmicenje
            {
                PredmetId = model.PredmetId,
                Datum = model.Datum,
                SkolaId=model.SkolaDomacinId,
                Razred=model.Razred,
                Zakljucan=false
            };
            _db.Add(_takmicenje);
            _db.SaveChanges();
            IEnumerable<OdjeljenjeStavka> Ucenici = _db.OdjeljenjeStavka.Where(x => x.DodjeljenPredmets
                .Where(y => y.PredmetId == _takmicenje.PredmetId).FirstOrDefault().ZakljucnoKrajGodine == 5 
                && x.DodjeljenPredmets.Average(y => y.ZakljucnoKrajGodine) > 4);
            var broj = Ucenici.Count();
            foreach (var i in Ucenici)
            {
                _db.Add(new TakmicenjeUcesnik
                {
                    Bodovi=0,
                    OdjeljenjeStavkaId=i.Id,
                    Pristupio=false,
                    TakmicenjeId=_takmicenje.Id,
                });
            }
            _db.SaveChanges();
            return RedirectToAction("Korak2", new Korak1 { SkolaID=  model.SkolaDomacinId,  Razred=model.Razred });
        }
        public IActionResult Korak4(int Id)
        {
            Takmicenje takmicenje = _db.Takmicenje.Include(x=> x.Predmet).Include(x=> x.Skola).Where(x=> x.Id==Id).FirstOrDefault();
            Korak4 ViewModel = new Korak4
            {
                SkolaDomacin = takmicenje.Skola.Naziv,
                Predmet= takmicenje.Predmet.Naziv,
                Razred=takmicenje.Razred,
                Datum = takmicenje.Datum.ToShortDateString(),
                TakmicenjeId=takmicenje.Id,
                SkolaDomacinId=takmicenje.SkolaId
            };
            return View(ViewModel);
        }
        public IActionResult Korak4PrikaziUcesnike(int Id)
        {
            Korak4PrikaziUcesnike ViewModel = new Korak4PrikaziUcesnike
            {
                TakmicenjeId = Id,
                Ucesnici = _db.TakmicenjeUcesnik.Where(x => x.TakmicenjeId == Id).Select(x => new Korak4PrikaziUcesnike.Ucesnik
                {
                    Bodovi = x.Bodovi,
                    BrojUDnevniku = x.OdjeljenjeStavka.BrojUDnevniku,
                    Odjeljenje = x.OdjeljenjeStavka.Odjeljenje.Oznaka,
                    Pristupio=x.Pristupio,
                    UcesnikId=x.Id
                })
            };
            return PartialView(ViewModel);
        }
        public IActionResult Promjena(int Id)
        {

            TakmicenjeUcesnik ucesnik = _db.TakmicenjeUcesnik.Find(Id);
            Takmicenje takmicenje = _db.Takmicenje.Find(ucesnik.TakmicenjeId);
            if (takmicenje.Zakljucan)
            {
                return Redirect("/Takmicenje/Korak4PrikaziUcesnike/" + ucesnik.TakmicenjeId);
            }
            ucesnik.Pristupio = !ucesnik.Pristupio;
            if (!ucesnik.Pristupio)
            {
                ucesnik.Bodovi = 0;
            }
            _db.SaveChanges(); 
            return Redirect("/Takmicenje/Korak4PrikaziUcesnike/"+ ucesnik.TakmicenjeId);
        }
        public IActionResult Zakljucaj(int Id)
        {
            Takmicenje takmicenje = _db.Takmicenje.Find(Id);
            takmicenje.Zakljucan = true;
            _db.SaveChanges();
            return Redirect("/Takmicenje/Korak4/" + Id);
        }
        public IActionResult Korak4Uredi(int Id)
        {
            Korak4Uredi ViewModel = _db.TakmicenjeUcesnik.Where(x => x.Id == Id).Select(x => new Korak4Uredi
            {
                Bodovi = x.Bodovi,
                ImeIPrezime = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                UcesnikId = x.Id
            }).SingleOrDefault();
            return PartialView(ViewModel);
        }
        public IActionResult Korak4UrediSnimi(Korak4Uredi model)
        {
            TakmicenjeUcesnik ucesnik = _db.TakmicenjeUcesnik.Find(model.UcesnikId);
            ucesnik.Pristupio = true;
            ucesnik.Bodovi = model.Bodovi;
            _db.SaveChanges();
            return Redirect("/Takmicenje/Korak4PrikaziUcesnike/" + ucesnik.TakmicenjeId);
        }

        public IActionResult Korak4DodajUcesnika(int id)
        {

            Korak4DodajUcesnika ViewModel = new Korak4DodajUcesnika { TakmicenjeId = id, OdjeljenjeStavka=_db.OdjeljenjeStavka.Include(x=> x.Odjeljenje).Include(x=> x.Ucenik).Select(x=> new SelectListItem {
                Text = x.Odjeljenje.Oznaka+" - "+x.Ucenik.ImePrezime,
                 Value=x.Id.ToString()
            })};
            
                
            return PartialView(ViewModel);
        }

        public IActionResult Korak4DodajUcesnikaSnimi(Korak4DodajUcesnika model)
        {
            _db.Add(new TakmicenjeUcesnik
            {
                Bodovi = model.Bodovi,
                TakmicenjeId = model.TakmicenjeId,
                Pristupio = true,
                OdjeljenjeStavkaId = model.OdjeljenjeStavkaId

            });
            _db.SaveChanges();
            return Redirect("/Takmicenje/Korak4PrikaziUcesnike/" + model.TakmicenjeId);
        }
    }
}