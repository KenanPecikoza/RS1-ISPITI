using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Ispit.Data;
using Ispit.Data.EntityModels;
using Ispit.Web.Helper;
using Ispit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ispit.Web.Controllers
{
    public class DetaljiDogadajaController : Controller
    {
        private readonly MyContext _db;
        public DetaljiDogadajaController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Index(int Id)
        {
            Korak2 viewModel = _db.OznacenDogadjaj.Where(x => x.ID == Id).Select(x => new Korak2
            {
                DatumDodavanja = x.DatumDodavanja.ToShortDateString(),
                DogadjajId = x.DogadjajID,
                Nastavnik = x.Dogadjaj.Nastavnik.ImePrezime,
                DatumDogadjaja = x.Dogadjaj.DatumOdrzavanja.ToShortDateString(),
                Opis = x.Dogadjaj.Opis
            }).FirstOrDefault();
            return View(viewModel);
        }
        public IActionResult PrikaziObaveze(int Id)
        {
            IEnumerable<Korak3> viewModel = _db.StanjeObaveze.Where(x => x.Obaveza.DogadjajID == Id).Select(x => new Korak3
            {
                DanaUnaprijed = x.NotifikacijaDanaPrije,
                Naziv = x.Obaveza.Naziv,
                Ponavljaj = x.NotifikacijeRekurizivno,
                Procenat = x.IzvrsenoProcentualno,
                ObavezaId = x.Id,
                DogadjajId=x.OznacenDogadjaj.DogadjajID
            });
            return PartialView(viewModel);
        }

        public IActionResult UrediObavezu(int Id)
        {
            Korak4 stanjeObaveze = _db.StanjeObaveze.Where(x=> x.Id==Id).Select(x=> new Korak4
            {
                Id=x.Id,
                Naziv=x.Obaveza.Naziv,
                procentualno=x.IzvrsenoProcentualno,
                DogadjajId=x.OznacenDogadjaj.DogadjajID
            }).FirstOrDefault();
            return PartialView(stanjeObaveze);
        }
        public IActionResult UrediSnimi(Korak4 model)
        {
            StanjeObaveze stanjeObaveze = _db.StanjeObaveze.Find(model.Id);
            stanjeObaveze.IzvrsenoProcentualno = model.procentualno;
            _db.SaveChanges();
            return RedirectToAction("PrikaziObaveze",new {Id= model.DogadjajId });
        }

    }
}