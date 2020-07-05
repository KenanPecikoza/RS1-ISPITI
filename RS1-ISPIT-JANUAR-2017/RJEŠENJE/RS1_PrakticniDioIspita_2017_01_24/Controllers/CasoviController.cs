using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using RS1_PrakticniDioIspita_2017_01_24.ViewModel;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class CasoviController : Controller
    {
        private readonly MojContext _db;
        public CasoviController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Korak1Nastavnici> viewModel = _db.Nastavnik.Select(x => new Korak1Nastavnici
            {
                Id = x.Id,
                Ime = x.Ime,
                Username = x.Username
            });

            return View("Korak1", viewModel);
        }
        public IActionResult Korak2(int Id)
        {
            Korak2Casovi viewModel = new Korak2Casovi{ Id=Id,Casovi= _db.OdrzaniCas.Where(x => x.Angazovan.NastavnikId == Id).Select(x => new Korak2Casovi.Row
            {
                datum=x.datum,
                Id=x.Id,
                Predmet=x.Angazovan.Predmet.Naziv,
                Oznaka=x.Angazovan.Odjeljenje.Oznaka,
            }) };

            return View(viewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 viewModel = _db.Nastavnik.Where(x => x.Id == Id).Select(x => new Korak3
            {
                Nastavnik = x.Ime,
                Id = x.Id,
                Datum=DateTime.Now
            }).FirstOrDefault();

            viewModel.OdjeljenjePredmet = _db.Angazovan.Where(x => x.NastavnikId == Id).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Odjeljenje.Oznaka + " / " + x.Predmet.Naziv
            });


            return View(viewModel);
        }
        public IActionResult Korak3Snimi(Korak3 Model)
        {
            OdrzaniCas odrzaniCas = new OdrzaniCas
            {
                AngazovanId = Model.AngazovanId,
                datum = Model.Datum
            };
            _db.Add(odrzaniCas);
            _db.SaveChanges();

            int OdjeljenjeID = _db.Angazovan.Find(Model.AngazovanId).OdjeljenjeId;
            foreach (var i in  _db.UpisUOdjeljenje.Where(x => x.OdjeljenjeId ==OdjeljenjeID))
            {
                _db.Add(new OdrzaniCasDetalj {
                    OdrzaniCasId = odrzaniCas.Id,
                    UpisUOdjeljenjeId = i.Id,
                });
            }
            _db.SaveChanges();
            return Redirect("/Casovi/Korak2/"+odrzaniCas.Angazovan.NastavnikId);
        }

    }
}
