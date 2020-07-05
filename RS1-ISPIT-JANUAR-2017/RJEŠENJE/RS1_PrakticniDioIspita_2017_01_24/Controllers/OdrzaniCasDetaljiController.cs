using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RS1_PrakticniDioIspita_2017_01_24.EF;
using RS1_PrakticniDioIspita_2017_01_24.Models;
using RS1_PrakticniDioIspita_2017_01_24.ViewModel;

namespace RS1_PrakticniDioIspita_2017_01_24.Controllers
{
    public class OdrzaniCasDetaljiController : Controller
    {
        private readonly MojContext _db;

        public OdrzaniCasDetaljiController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Korak4(int Id)
        {
            Korak3 viewModel = _db.OdrzaniCas.Where(x => x.Id == Id).Select(x => new Korak3
            {
                Nastavnik = x.Angazovan.Nastavnik.Ime,
                Id = x.Id,
                Datum = x.datum,
                AngazovanId=x.AngazovanId,
            }).FirstOrDefault();

            int nastavnikID = _db.OdrzaniCas.Where(x => x.Id == Id).Select(x => x.Angazovan.NastavnikId).FirstOrDefault();
            viewModel.OdjeljenjePredmet = _db.Angazovan.Where(x => x.NastavnikId == nastavnikID).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Odjeljenje.Oznaka + " / " + x.Predmet.Naziv
            });
            return View(viewModel);
        }
        public IActionResult Korak4Detalji(int Id)
        {
           IEnumerable<Korak4>viewModel=_db.OdrzaniCasDetalj.Where(x=> x.OdrzaniCasId==Id).Select(x=> new Korak4 { 
                Id=x.Id,
                Ocjena=x.Ocjena?? 0,
                Opravdano=x.OpravdanoOdsutan?? false,
                Odsutan=x.Odsutan,
                Ucenik=x.UpisUOdjeljenje.Ucenik.Ime
            });

            return PartialView("Korak4Ajax",viewModel);
        }
        public IActionResult Promjena(int Id)
        {
            OdrzaniCasDetalj ucenik = _db.OdrzaniCasDetalj.Find(Id);
            ucenik.Odsutan = !ucenik.Odsutan;
            _db.SaveChanges();
            return Redirect("/OdrzaniCasDetalji/Korak4Detalji/" + ucenik.OdrzaniCasId);
        }

        public IActionResult Detalji(int Id)
        {
            Detalji viewModel = _db.OdrzaniCas.Where(x => x.Id == Id).Select(x => new Detalji
            {
                Datum = x.datum.ToShortDateString(),
                Id = x.Id,
                Odjeljenje = x.Angazovan.Odjeljenje.Oznaka,
                Predmet = x.Angazovan.Predmet.Naziv,
                Prisutno = _db.OdrzaniCasDetalj.Where(y => y.OdrzaniCasId == Id).Count(y => !y.Odsutan),
                Ukupno = _db.OdrzaniCasDetalj.Where(y => y.OdrzaniCasId == Id).Count(),
                NajboljiUcenik = _db.OdrzaniCasDetalj.Where(y => y.OdrzaniCasId == Id).OrderByDescending(y => y.Ocjena).Select(y=> y.UpisUOdjeljenje.Ucenik.Ime).FirstOrDefault()
            }).FirstOrDefault();
            return PartialView(viewModel);
        }

        public IActionResult Korak4Uredi(int Id)
        {
            Korak4 viewModel = _db.OdrzaniCasDetalj.Where(x => x.Id == Id).Select(x => new Korak4
            {
                Id = x.Id,
                Ocjena = x.Ocjena ?? 0,
                Odsutan = x.Odsutan,
                Opravdano = x.OpravdanoOdsutan ?? false,
                Ucenik = x.UpisUOdjeljenje.Ucenik.Ime
            }).FirstOrDefault();
            return PartialView(viewModel);
        }

        public IActionResult Korak4UrediSnimi(Korak4 model)
        {
            OdrzaniCasDetalj ucenik = _db.OdrzaniCasDetalj.Find(model.Id);
            if (model.Odsutan)
            {
                ucenik.OpravdanoOdsutan = model.Opravdano;
            }
            else
            {
                ucenik.Ocjena = model.Ocjena;
            }
            _db.SaveChanges();
            return Redirect("/OdrzaniCasDetalji/Korak4Detalji/" + ucenik.OdrzaniCasId);
        }
    }
}
