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
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Ispit.Web.Controllers
{
    [Autorizacija]

    public class OznaceniDogadajiController : Controller
    {
        private readonly MyContext _db;
        public OznaceniDogadajiController(MyContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            Korak1 viewModel = new Korak1
            {
                OznaceniDogadjaji = _db.OznacenDogadjaj.Where(x => x.StudentID == HttpContext.GetLogiraniKorisnik().Id).Select(x => new Korak1.Oznaceni
                {
                    Datum = x.DatumDodavanja.ToShortDateString(),
                    DogadjajId = x.ID,
                    NastavnikImeIPrezime = x.Dogadjaj.Nastavnik.ImePrezime,
                    OpisDogadjaja = x.Dogadjaj.Opis,
                    Realizovano =Math.Round(_db.StanjeObaveze.Where(y => y.OznacenDogadjajID == x.ID).Sum(y => y.IzvrsenoProcentualno) / _db.StanjeObaveze.Count(y => y.OznacenDogadjajID == x.ID),2)
                }),
                NeoznaceniDogadjaji = _db.Dogadjaj.Include(x=> x.Nastavnik)
                .Except(_db.OznacenDogadjaj.Where(x => x.StudentID == HttpContext.GetLogiraniKorisnik().Id).Select(x=> x.Dogadjaj).ToList()).ToList()
                .Select(x => new Korak1.Neoznaceni
                {
                    Datum = x.DatumOdrzavanja.ToShortDateString(),
                    DogadjajId = x.ID,
                    NastavnikImeIPrezime = x.Nastavnik.ImePrezime,
                    OpisDogadjaja = x.Opis,
                    Obaveza = _db.Obaveza.Count(y => y.DogadjajID == x.ID)
                })
            }; 
            return View(viewModel);
        }

        public IActionResult Dodaj(int Id)
        {
            OznacenDogadjaj dogadjaj= new OznacenDogadjaj
            {
                DatumDodavanja = DateTime.Now,
                DogadjajID = Id,
                StudentID = HttpContext.GetLogiraniKorisnik().Id
            };
            _db.Add(dogadjaj);
            _db.SaveChanges();

            IEnumerable<Obaveza> obaveze = _db.Obaveza.Where(x => x.DogadjajID == Id);
            foreach (var i in obaveze)
            {
                StanjeObaveze stanje = new StanjeObaveze
                {
                    DatumIzvrsenja = DateTime.Now,
                    IsZavrseno = false,
                    IzvrsenoProcentualno = 33,
                    NotifikacijaDanaPrije = 4,
                    ObavezaID = i.ID,
                    OznacenDogadjajID = dogadjaj.ID,
                    NotifikacijeRekurizivno = true,
                };
                _db.Add(stanje);
            }
                _db.SaveChanges();
            return Redirect("/OznaceniDogadaji");
        }

    }
}