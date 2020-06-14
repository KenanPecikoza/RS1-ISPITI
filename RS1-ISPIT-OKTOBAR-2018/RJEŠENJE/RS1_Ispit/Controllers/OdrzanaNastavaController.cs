using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModel;
using SQLitePCL;

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
                ImeIPrezimeNastavnika = x.Ime + " " + x.Prezime,
                NastavnikId = x.Id,
                Skola = x.Skola.Naziv
            });
            return View("Korak1", viewModel);
        }
        public IActionResult Korak2(int Id)
        {
            Korak2 viewModel = new Korak2 { NastavnikId = Id, NastavnikSkola = _db.Nastavnik.Where(x => x.Id == Id).Select(x => x.Ime + " " + x.Prezime + " | " + x.Skola.Naziv).FirstOrDefault() };
            viewModel.rows = _db.OdrzaniCas.Where(x => x.PredajePredmet.Nastavnik.Id == Id).Select(x => new Korak2.Row
            {
                Datum = x.Datum.ToShortDateString(),
                OdrzaniCasId = x.Id,
                Predmet = x.PredajePredmet.Predmet.Naziv,
                SkolskaGodinaOdjeljenje = x.PredajePredmet.Odjeljenje.SkolskaGodina.Naziv,
                Odsutni = _db.OdrzaniCasStavka.Where(y => y.OdrzaniCasId == x.Id && !y.Prisutan).Select(y => y.OdjeljenjeStavka.Ucenik.ImePrezime)
            });

            return View(viewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 viewModel = _db.Nastavnik.Where(x => x.Id == Id).Select(x => new Korak3
            {
                Datum = DateTime.Now,
                Nastavnik = x.Ime + " " + x.Prezime,
                NastavnikId=x.Id
            }).FirstOrDefault();
            viewModel.OdjeljenjePremeti = _db.PredajePredmet.Where(x => x.NastavnikID == Id).Select(x => new SelectListItem
            {
                Value=x.Id.ToString(),
                Text=x.Odjeljenje.Razred+"-"+x.Odjeljenje.Oznaka+" / "+ x.Predmet.Naziv
            });

            return View(viewModel);
        }
        public IActionResult Korak3Snimi(Korak3 model)
        {
            OdrzaniCas cas = new OdrzaniCas
            {
                Datum = model.Datum,
                PredajePredmetId = model.PredajePredmetId
            };
            _db.Add(cas);
            _db.SaveChanges();

            IEnumerable<OdrzaniCasStavka> ucenici= _db.DodjeljenPredmet.Where(x => x.PredmetId == _db.PredajePredmet.Where(y=> y.Id==model.PredajePredmetId).FirstOrDefault().PredmetID).Select(x => new OdrzaniCasStavka
            {
                Ocjena = 0,
                OdjeljenjeStavkaId = x.OdjeljenjeStavkaId,
                OdrzaniCasId = cas.Id,
                Opravdano = false,
                Prisutan = false
            });
            foreach (var i in ucenici)
            {
                _db.OdrzaniCasStavka.Add(i);
            }
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak2/" + model.NastavnikId);
        }

        public IActionResult Korak4(int Id)
        {
            Korak4 viewModel = _db.OdrzaniCas.Where(x => x.Id == Id).Select(x => new Korak4
            {
                Datum = x.Datum.ToShortDateString(),
                Odjeljenje=x.PredajePredmet.Odjeljenje.Oznaka+" "+x.PredajePredmet.Odjeljenje.Razred,
                OdrzaniCasId=x.Id,
            }).FirstOrDefault();

            return View(viewModel);
        }
        public IActionResult Korak4Snimi(Korak4 Model)
        {
            OdrzaniCas cas = _db.OdrzaniCas.Find(Model.OdrzaniCasId);
            cas.SadrzajCasa = Model.SadrzajCasa;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4/" + Model.OdrzaniCasId);
        }
        public IActionResult Korak4PrikaziUcenike(int Id)
        {
            IEnumerable<Korak4PrikaziUcenike> viewModel = _db.OdrzaniCasStavka.Where(x => x.OdrzaniCasId == Id).Select(x => new Korak4PrikaziUcenike
            {
                Ocjena = x.Ocjena,
                Prisutan = x.Prisutan,
                Ucenik = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                Opravdano = x.Opravdano,
                OdrzaniCasStavkaId=x.Id
            });
            return PartialView(viewModel);
        }
        public IActionResult Promjena(int Id)
        {
            OdrzaniCasStavka ucenik = _db.OdrzaniCasStavka.Find(Id);
            ucenik.Prisutan = !ucenik.Prisutan;
            if (ucenik.Prisutan)
                ucenik.Opravdano = false;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/"+ucenik.OdrzaniCasId);
        }
        public IActionResult Uredi(int Id)
        {
            OdrzaniCasStavka ucenik = _db.OdrzaniCasStavka.Include(x=> x.OdjeljenjeStavka).ThenInclude(x=> x.Ucenik).Where(x=> x.Id==Id).FirstOrDefault();
            if (ucenik.Prisutan)
            {
                Korak4UrediPrisutan viewModelPrisutan = new Korak4UrediPrisutan
                {
                    Ocjena = ucenik.Ocjena,
                    Ucenik = ucenik.OdjeljenjeStavka.Ucenik.ImePrezime,
                    OdrzaniCasStavkaId = ucenik.Id      
                };
                return View("Korak4UrediPrisutan", viewModelPrisutan);
            }
            Korak4UrediOdsutan viewModelOdsutan = new Korak4UrediOdsutan
            {
                Napomena=ucenik.Napomena,
                Opravdano=ucenik.Opravdano,
                Ucenik=ucenik.OdjeljenjeStavka.Ucenik.ImePrezime,
                OdrzaniCasStavkaId=ucenik.Id
            };
            return View("Korak4UrediOdsutan", viewModelOdsutan);
        }
        public IActionResult Korak4UrediPrisutanSnimi(Korak4UrediPrisutan model)
        {
            OdrzaniCasStavka ucenik = _db.OdrzaniCasStavka.Find(model.OdrzaniCasStavkaId);
            ucenik.Ocjena = model.Ocjena;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + ucenik.OdrzaniCasId);
        }
        public IActionResult Korak4UrediOdsutanSnimi(Korak4UrediOdsutan model)
        {
            OdrzaniCasStavka ucenik = _db.OdrzaniCasStavka.Find(model.OdrzaniCasStavkaId);
            ucenik.Opravdano = model.Opravdano;
            ucenik.Napomena = model.Napomena;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + ucenik.OdrzaniCasId);
        }
    }
}