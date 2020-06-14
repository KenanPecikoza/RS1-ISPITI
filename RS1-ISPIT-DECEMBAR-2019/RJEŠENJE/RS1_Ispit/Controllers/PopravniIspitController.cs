using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModel;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class PopravniIspitController : Controller
    {
        private readonly MojContext _db;
        public PopravniIspitController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Korak1 viewModel = new Korak1
            {
                Skole = _db.Skola.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }),
                Predmeti = _db.Predmet.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }),
                SkolskeGodine = _db.SkolskaGodina.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }),
            };
            return View("Korak1", viewModel);
        }
        public IActionResult Korak2(Korak1 model)
        {
            Korak2 viewModel = new Korak2 { PredmetId = model.PredmetId, SkolaId = model.SkolaId, SkolskaGodinaId = model.SkolskaGodinaId,
                                            Predmet=_db.Predmet.Find(model.PredmetId).Naziv, Skola=_db.Skola.Find(model.SkolaId).Naziv, SkolskaGodina=_db.SkolskaGodina.Find(model.SkolskaGodinaId).Naziv   };
            viewModel.Rows = _db.PopravniIspit.Where(x => x.SkolskaGodinaId == model.SkolskaGodinaId && x.PredmetId == model.PredmetId && x.SkolskaGodinaId == model.SkolskaGodinaId).Select(x => new Korak2.Row
            {
                Datum = x.Datum.ToShortDateString(),
                Nastavnik = x.ClanKomisije1.Ime + " " + x.ClanKomisije1.Prezime,
                PopravniIspitId = x.Id,
                BrojUčenika = _db.PopravniIspitStavka.Count(y => y.PopravniIspitId == x.Id),
                BrojUcenikaKojiSuPolozili = _db.PopravniIspitStavka.Count(y => y.PopravniIspitId == x.Id && y.Bodovi > 50),
            });

            return View(viewModel);
        }
        public IActionResult Korak3(int SkolaId, int PredmetId, int SkolskaGodinaId)
        {
            Korak3 viewModel = new Korak3
            {
                SkolskaGodinaId=SkolskaGodinaId,
                PremdetId=PredmetId,
                SkolaId=SkolaId,
                Skola=_db.Skola.Find(SkolaId).Naziv,
                Predmet=_db.Predmet.Find(PredmetId).Naziv,
                SkolskaGodina=_db.SkolskaGodina.Find(SkolskaGodinaId).Naziv,
                Datum=DateTime.Now,
                ClanoviKomisije =_db.Nastavnik.Select(x=> new SelectListItem
                {
                    Value=x.Id.ToString(),
                    Text=x.Ime+" "+x.Prezime 
                })

            };
            return View(viewModel);
        }
        public IActionResult Korak3Snimi(Korak3 model)
        {
            PopravniIspit popravni = new PopravniIspit
            {
                 ClanKomisije1Id=model.ClanKomisije1Id,
                 ClanKomisije2Id=model.ClanKomisije2Id,
                 ClanKomisije3Id=model.ClanKomisije3Id,
                 Datum=model.Datum,
                 PredmetId=model.PremdetId,
                 SkolskaGodinaId=model.SkolskaGodinaId,
                 SkolaId=model.SkolaId
            };
            _db.Add(popravni);
            _db.SaveChanges();
            IEnumerable<PopravniIspitStavka> ucenici = _db.DodjeljenPredmet.Where(x => x.ZakljucnoKrajGodine < 2 && x.PredmetId == model.PremdetId).Select(x => new PopravniIspitStavka
            {
                OdjeljenjeStavkaId = x.OdjeljenjeStavkaId,
                PopravniIspitId = popravni.Id,
                Pristupio = true,
                BrojUDnevniku=x.OdjeljenjeStavka.BrojUDnevniku,
                Izlazak=true,
            });
            foreach (var i in ucenici)
            {
                if (_db.DodjeljenPredmet.Where(x => x.OdjeljenjeStavkaId == i.OdjeljenjeStavkaId).Count(x => x.ZakljucnoKrajGodine <2) >= 3)
                {
                    i.Izlazak = false;
                    i.Bodovi = 0;
                }
                _db.Add(i);
            }
            _db.SaveChanges();
            return RedirectToAction("Korak2", new Korak1 { PredmetId=model.PremdetId,SkolskaGodinaId=model.SkolskaGodinaId,SkolaId=model.SkolaId} );
        }
        public IActionResult Korak4(int Id)
        {
            PopravniIspit popravniIspit = _db.PopravniIspit.Find(Id);
            Korak3 viewModel = new Korak3
            {
                SkolskaGodinaId = popravniIspit.SkolskaGodinaId,
                PremdetId = popravniIspit.PredmetId,
                SkolaId = popravniIspit.SkolaId,
                Skola = _db.Skola.Find(popravniIspit.SkolaId).Naziv,
                Predmet = _db.Predmet.Find(popravniIspit.PredmetId).Naziv,
                SkolskaGodina = _db.SkolskaGodina.Find(popravniIspit.SkolskaGodinaId).Naziv,
                Datum = popravniIspit.Datum,
                ClanKomisije1Id=popravniIspit.ClanKomisije1Id,
                ClanKomisije2Id=popravniIspit.ClanKomisije2Id,
                ClanKomisije3Id=popravniIspit.ClanKomisije3Id,
                ClanoviKomisije = _db.Nastavnik.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime + " " + x.Prezime
                }),
                PopravniIspitId=popravniIspit.Id
            };
            return View("Korak4",viewModel);
        }
        public IActionResult Korak4PrikaziUcenike(int Id)
        {
            IEnumerable<Korak4PrikaziUcenike> viewModel = _db.PopravniIspitStavka.Where(x => x.PopravniIspitId == Id).Select(x => new Korak4PrikaziUcenike
            {
                Bodovi=x.Bodovi,
                BrojUDnevniku=x.BrojUDnevniku,
                Izlazak=x.Izlazak,
                Odjeljenje=x.OdjeljenjeStavka.Odjeljenje.Oznaka,
                Prisutan=x.Pristupio,
                Ucenik=x.OdjeljenjeStavka.Ucenik.ImePrezime,
                PopravniIspitStavkaId=x.Id
            });
            return PartialView(viewModel);
        }
        public IActionResult Promjena(int Id)
        {
            PopravniIspitStavka model = _db.PopravniIspitStavka.Find(Id);
            model.Pristupio = !model.Pristupio;
            if (!model.Pristupio)
            {
                model.Bodovi = 0;
            }
            _db.SaveChanges();

            return Redirect("/PopravniIspit/Korak4PrikaziUcenike/"+model.PopravniIspitId);
        }
        public IActionResult Korak4Uredi(int Id)
        {
            PopravniIspitStavka model = _db.PopravniIspitStavka.Include(x=> x.OdjeljenjeStavka).ThenInclude(x=> x.Ucenik).Where(x=> x.Id==Id).FirstOrDefault();
            Korak4Uredi viewModel = new Korak4Uredi
            {
                Bodovi = model.Bodovi,
                PopravniIspitStavkaId = model.Id,
                Ucenik = model.OdjeljenjeStavka.Ucenik.ImePrezime
            };
            return PartialView(viewModel);
        }
        public IActionResult Korak4UrediSnimi(Korak4Uredi model)
        {
            PopravniIspitStavka ucenik = _db.PopravniIspitStavka.Find(model.PopravniIspitStavkaId);
            ucenik.Bodovi = model.Bodovi;
            ucenik.Pristupio = true;
            _db.SaveChanges();
            return Redirect("/PopravniIspit/Korak4PrikaziUcenike/" + ucenik.PopravniIspitId);
        }

    }
}