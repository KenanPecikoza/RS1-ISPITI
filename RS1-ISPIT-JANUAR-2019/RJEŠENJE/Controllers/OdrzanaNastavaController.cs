using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModel;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class OdrzanaNastavaController : Controller
    {
        private readonly MojContext _db;
        public OdrzanaNastavaController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Korak1> viewModel = _db.Nastavnik.Select(x => new Korak1
            {
                Id = x.Id,
                Nastavnik = x.Ime + " " + x.Prezime,
                Skola = _db.PredajePredmet.Where(y => y.NastavnikID == x.Id).Select(y => y.Odjeljenje.Skola.Naziv).FirstOrDefault()
            });

            return View("Korak1",viewModel);
        }
        public IActionResult Korak2(int Id)
        {
            Korak2 viewModel = new Korak2 {
                NastavnikId = Id,
                maturski = _db.MaturskiIspit.Where(x => x.NastavnikId == Id).Select(x => new Korak2.MaturskiIspiti
                {
                    Datum = x.Datum.ToShortDateString(),
                    MaturskiId=x.Id,
                    Predmet=x.Predmet.Naziv,
                    Skola=x.Skola.Naziv,
                    NisuPristupili=_db.MaturskiIspitStavka.Where(y=> y.MaturskiIspitId==x.Id).Select(y=> y.OdjeljenjeStavka.Ucenik.ImePrezime)
                }) 
            };
            return View(viewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 viewModel = new Korak3
            {
                Datum = DateTime.Now,
                SkolskaGodina = _db.SkolskaGodina.Where(x => x.Aktuelna).FirstOrDefault().Naziv,
                SkolskaGodinaId = _db.SkolskaGodina.Where(x => x.Aktuelna).FirstOrDefault().Id,
                NastavnikId = Id,
                Nastavnik = _db.Nastavnik.Where(x => x.Id == Id).Select(x => x.Ime + "  " + x.Prezime).FirstOrDefault(),
                Predmeti = _db.PredajePredmet.Where(x => x.NastavnikID == Id && x.Odjeljenje.Razred == 4 && x.Odjeljenje.SkolskaGodina.Aktuelna).Select(x => new SelectListItem
                {
                    Text = x.Predmet.Naziv,
                    Value = x.PredmetID.ToString()
                }),
                Skole = _db.Skola.Select(x => new SelectListItem
                {
                    Text=x.Naziv,
                    Value=x.Id.ToString()
                })
            };
            return View(viewModel);
        }
        public IActionResult Korak3Snimi(Korak3 model)
        {
            MaturskiIspit maturskiIspit = new MaturskiIspit
            {
                Datum = model.Datum,
                NastavnikId = model.NastavnikId,
                PredmetId = model.PredmetId,
                SkolaId = model.SkolaId,
                SkolskaGodinaId = model.SkolskaGodinaId,
            };
            _db.Add(maturskiIspit);
            _db.SaveChanges();
            IEnumerable<MaturskiIspitStavka> ucenici = _db.OdjeljenjeStavka.
                Except(_db.DodjeljenPredmet.Where(x => x.ZakljucnoKrajGodine == 1 && x.OdjeljenjeStavka.Odjeljenje.Razred < 4).Select(y => y.OdjeljenjeStavka)).ToList().
                Except(_db.MaturskiIspitStavka.Where(x=> x.RezultatiMaturskog>=55).Select(x=> x.OdjeljenjeStavka)).ToList().
                Select(x => new MaturskiIspitStavka
                {
                    MaturskiIspitId=maturskiIspit.Id,
                    OdjeljenjeStavkaId=x.Id,
                    Pristupio=false,
                    ProsjekOcjena=_db.DodjeljenPredmet.Where(y=> y.OdjeljenjeStavkaId==x.Id).Average(y=> y.ZakljucnoKrajGodine),
                    RezultatiMaturskog=0
                });
            foreach (var i in ucenici)
            {
                _db.Add(i);
            }
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak2/"+model.NastavnikId);
        }

        public IActionResult Korak4(int Id)
        {
            Korak4 viewModel = _db.MaturskiIspit.Where(x => x.Id == Id).Select(x => new Korak4
            {
                Datum = x.Datum.ToShortDateString(),
                Predmet = x.Predmet.Naziv,
                Id = x.Id,
                Napomena = x.Napomena
            }).FirstOrDefault();

            return View(viewModel);
        }

        public IActionResult Korak4Snimi(Korak4 model)
        {
            MaturskiIspit maturski = _db.MaturskiIspit.Find(model.Id);
            maturski.Napomena = model.Napomena;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4/" + model.Id);
        }
        public IActionResult Korak4PrikaziUcenike(int Id)
        {
            IEnumerable<Korak4PrikaziUcenike> viewModel = _db.MaturskiIspitStavka.Where(x => x.MaturskiIspitId == Id).Select(x => new Korak4PrikaziUcenike
            {
                Pristupio=x.Pristupio,
                Prosjek=x.ProsjekOcjena,
                RezultatiMaturskog=x.RezultatiMaturskog,
                Ucenik=x.OdjeljenjeStavka.Ucenik.ImePrezime,
                StavkaId=x.Id
            });
            return PartialView(viewModel);
        }
        public IActionResult Promjena(int Id)
        {
            MaturskiIspitStavka model = _db.MaturskiIspitStavka.Find(Id);
            model.Pristupio = !model.Pristupio;
            if (!model.Pristupio)
            {
                model.RezultatiMaturskog = 0;
            }
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/"+model.MaturskiIspitId);
        }
        public IActionResult Korak4Uredi(int Id)
        {
            Korak4Uredi viewModel = _db.MaturskiIspitStavka.Where(x => x.Id == Id).Select(x => new Korak4Uredi
            {
                Bodovi=x.RezultatiMaturskog,
                Ucenik = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                StavkaId = x.Id
            }).FirstOrDefault();
            return PartialView(viewModel);
        }

        public IActionResult Korak4UrediSnimi(Korak4Uredi model)
        {
            MaturskiIspitStavka stavka = _db.MaturskiIspitStavka.Find(model.StavkaId);
            stavka.RezultatiMaturskog = model.Bodovi;
            stavka.Pristupio = true;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + stavka.MaturskiIspitId);
        }
        public IActionResult Korak4BodoviSnimi(double Bodovi, int StavkaId)
        {
            MaturskiIspitStavka stavka = _db.MaturskiIspitStavka.Find(StavkaId);
            stavka.RezultatiMaturskog = Bodovi;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + stavka.MaturskiIspitId);
        }
    }
}