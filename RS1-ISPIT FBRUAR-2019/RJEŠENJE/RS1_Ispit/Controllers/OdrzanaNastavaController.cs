using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.Migrations;
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
                BrojCasova = _db.OdrzaniCas.Count(y => y.PredajePredmet.Odjeljenje.SkolskaGodina.Aktuelna && y.PredajePredmet.NastavnikID == x.Id),
                ImeIPrezime = x.Ime + " " + x.Prezime,
                NastavnikId = x.Id
            });
            return View("Korak1",viewModel);
        }

        public IActionResult Korak2(int Id)
        {

            Korak2 viewModel = new Korak2 { NastavnikId = Id };
            viewModel.Red= _db.OdrzaniCas.Where(x => x.PredajePredmet.NastavnikID == Id).Select(x => new Korak2.Row
            {
                Datum = x.Datum.ToShortDateString(),
                Predmet = x.PredajePredmet.Predmet.Naziv,
                SkolskaGodinaOdjeljenje = x.PredajePredmet.Odjeljenje.SkolskaGodina.Naziv + "/" + x.PredajePredmet.Odjeljenje.Oznaka,
                Skola = x.PredajePredmet.Odjeljenje.Skola.Naziv,
                OdrzaniCasId = x.ID,
                Odsutni = _db.OdrzaniCasDetalji.Where(y => y.OdrzaniCasId == x.ID && !y.Prisutan).Select(y=> y.OdjeljenjeStavka.Ucenik.ImePrezime)
            });
            return View(viewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 viewModel = _db.Nastavnik.Where(x => x.Id == Id).Select(x => new Korak3
            {
                NastavnikImeIPrezime = x.Ime + " " + x.Prezime,
                Datum = DateTime.Now,
                NastavnikId = Id
            }).FirstOrDefault();

            viewModel.SkolaOdjeljenjePredmet = _db.PredajePredmet.Where(x => x.NastavnikID == Id && x.Odjeljenje.SkolskaGodina.Aktuelna).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Odjeljenje.Skola.Naziv + " / " + x.Odjeljenje.Oznaka + " / " + x.Predmet.Naziv
            }).ToList();
            return View(viewModel);
        }
        public IActionResult SnimiKorak3(Korak3 Model)
        {
            //promijeniti prije pusha na github migraciju
            EntityModels.OdrzaniCas noviCas = new EntityModels.OdrzaniCas
            {
                Datum = Model.Datum,
                PredajePredmetID = Model.PredajePredmetId,
                SadrzajCasa = Model.SadrzajCasa,
            };
            _db.Add(noviCas);
            _db.SaveChanges();

            var Ucenici = _db.OdjeljenjeStavka.
                Where(x => x.OdjeljenjeId == _db.PredajePredmet.Where(y => y.Id == noviCas.PredajePredmetID).FirstOrDefault().OdjeljenjeID).ToList();
            foreach (var i in Ucenici)
            {
                OdrzaniCasDetalji detalji = new OdrzaniCasDetalji
                {
                    OdjeljenjeStavkaID = i.Id,
                    OdrzaniCasId = noviCas.ID,
                    Prisutan = true,
                };
                _db.Add(detalji);
                _db.SaveChanges();
            }
            return Redirect("/OdrzanaNastava/Korak2/"+ Model.NastavnikId);
        }
        public IActionResult Korak4(int Id)
        {
            Korak4 viewModel = _db.OdrzaniCas.Where(x => x.ID == Id).Select(x => new Korak4
            {
                Datum = x.Datum,
                SadrzajCasa = x.SadrzajCasa,
                OdjeljenjePredmet = x.PredajePredmet.Odjeljenje.Oznaka + " / " + x.PredajePredmet.Predmet.Naziv,
                OdrzaniCasId = Id
            }).FirstOrDefault();

            return View(viewModel);
        }
        public IActionResult Korak4PrikaziUcenike(int Id)
        {
            Korak4PrikaziUcenike viewModel = new Korak4PrikaziUcenike { OdrzaniCasId = Id };
            viewModel.rows = _db.OdrzaniCasDetalji.Where(x => x.OdrzaniCasId == Id).Select(x => new Korak4PrikaziUcenike.Row
            {
                ImeIPrezime = x.OdjeljenjeStavka.Ucenik.ImePrezime,
                Ocjena = x.Ocjena,
                Opravdano = x.Opravdano,
                Prisutan = x.Prisutan,
                DetaljID = x.Id
            });

            return PartialView(viewModel);
        }
        public IActionResult Promjena(int Id)
        {
            OdrzaniCasDetalji model = _db.OdrzaniCasDetalji.Where(x => x.Id == Id).SingleOrDefault();
            if ((model.Ocjena != null || model.Ocjena == 0)&& model.Prisutan)
            {
                return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + model.OdrzaniCasId);
            }
            model.Prisutan = !model.Prisutan;
            if (model.Prisutan){
                model.Opravdano = null;
            }
            model.Opravdano = false;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/"+ model.OdrzaniCasId );
        }
        public IActionResult PromjenaOpravdano(int Id)
        {
            OdrzaniCasDetalji model = _db.OdrzaniCasDetalji.Where(x => x.Id == Id).SingleOrDefault();
            model.Opravdano = !model.Opravdano;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + model.OdrzaniCasId);
        }

        public IActionResult Korak4Uredi(int Id)
        {
            OdrzaniCasDetalji model = _db.OdrzaniCasDetalji.Include(x=> x.OdjeljenjeStavka).Include(x=> x.OdjeljenjeStavka.Ucenik).Where(x => x.Id == Id).SingleOrDefault();
            Korak4Uredi viewModel = new Korak4Uredi { DetaljiID = Id, OdrzaniCasId = model.OdrzaniCasId, PrisutanNaCasu = model.Prisutan, Ucenik=model.OdjeljenjeStavka.Ucenik.ImePrezime };
            if (model.Prisutan)
            {
                viewModel.UcenikPrisutan = _db.OdrzaniCasDetalji.Where(x => x.OdrzaniCasId == Id).Select(x => new Korak4Uredi.Prisutan
                {
                    Ocjena = model.Ocjena
                }).FirstOrDefault();
                return PartialView(viewModel);
            }
            viewModel.UcenikOdsutan = _db.OdrzaniCasDetalji.Where(x => x.OdrzaniCasId == Id).Select(x => new Korak4Uredi.Odsutan
            {
                Napomena=model.Napomena,
                Opravdano=model.Opravdano?? false
            }).FirstOrDefault();
            return PartialView(viewModel);
        }
        public IActionResult SnimiKorak4(Korak4Uredi model)
        {
            var ucenik = _db.OdrzaniCasDetalji.Where(x => x.Id == model.DetaljiID).SingleOrDefault();
            if (ucenik.Prisutan)
            {
                ucenik.Ocjena = model.UcenikPrisutan.Ocjena;
                _db.SaveChanges();
                return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + model.OdrzaniCasId);
            }
            ucenik.Opravdano = model.UcenikOdsutan.Opravdano;
            ucenik.Napomena = model.UcenikOdsutan.Napomena;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + model.OdrzaniCasId);
        }
        public IActionResult UrediBodove(int DetaljiId, int Ocjena)
        {
            var model = _db.OdrzaniCasDetalji
                .Where(x => x.Id == DetaljiId).SingleOrDefault();
            model.Ocjena = Ocjena;
            _db.SaveChanges();
            return Redirect("/OdrzanaNastava/Korak4PrikaziUcenike/" + model.OdrzaniCasId);
        }

    }
}