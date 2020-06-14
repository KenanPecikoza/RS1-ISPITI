using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.Web.CodeGeneration;
using RS1_Ispit_asp.net_core.EF;
using RS1_Ispit_asp.net_core.EntityModels;
using RS1_Ispit_asp.net_core.ViewModel;

namespace RS1_Ispit_asp.net_core.Controllers
{
    public class IspitController : Controller
    {
        readonly MojContext  _db;
        public IspitController(MojContext db)
        {
            _db = db;
        }
        Korak3 IzvuciIspit(int Id)
        {
            Korak3 viewModel = _db.Angazovan.Where(x => x.Id == Id).Select(x => new Korak3
            {
                AngazovanId = x.Id,
                Datum = DateTime.Now,
                Nastavnik = x.Nastavnik.Ime + " " + x.Nastavnik.Prezime,
                Godina = x.AkademskaGodina.Opis,
                Predmet = x.Predmet.Naziv
            }).FirstOrDefault();
            return viewModel;
        }
        public IActionResult Index()
        {
            IEnumerable<Korak1> viewModel = _db.Predmet.Select(x => new Korak1
            {
                NazivPredmeta = x.Naziv,
                Rows = _db.Angazovan.Where(y => y.PredmetId == x.Id).Select(y => new Korak1.Row
                {
                    AkademskaGodina = y.AkademskaGodina.Opis,
                    AngazovanId = y.Id,
                    ImeIPrezimeNastavnika = y.Nastavnik.Ime + " " + y.Nastavnik.Prezime,
                    BrojOdrzanihCasova = _db.OdrzaniCas.Count(z => z.AngazovaniId == y.Id),
                    BrojStudenataNaPredmetu = _db.SlusaPredmet.Count(z => z.AngazovanId == y.Id),
                })
            }); 
            return View("Korak1", viewModel);
        }
        public IActionResult Korak2(int Id)
        {
            Korak2 viewModel = _db.Angazovan.Where(x => x.Id == Id).Select(x => new Korak2
            {
                AkademskaGodina = x.AkademskaGodina.Opis,
                Predmet = x.Predmet.Naziv,
                ImeIPrezimeNastavnika = x.Nastavnik.Ime + " " + x.Nastavnik.Prezime,
                AngazovanId = x.Id,
                Rows = _db.Ispit.Where(y => y.AngazovanId == x.Id).Select(y => new Korak2.Row
                {
                    BrojPrijavljenihStudenata = _db.IspitStudent.Count(z => z.IspitId==y.Id && z.Prijava),
                    BrojStudenataKojiNisuPolozili = _db.IspitStudent.Count(z => z.IspitId==y.Id && z.Polozio==false),
                    EvidentiraniRezultati=y.Zaključano,
                    DatumIspita=y.Datum.ToShortDateString(),
                    IspitId=y.Id
                })
            }).FirstOrDefault();
            return View(viewModel);
        }
        public IActionResult Korak3(int Id)
        {
            Korak3 viewModel = IzvuciIspit(Id);
            return View(viewModel);
        }
        public IActionResult Korak3Snimi(Korak3 model)
        {
            Ispit ispit = new Ispit
            {
                AngazovanId = model.AngazovanId,
                Datum = model.Datum,
                Napomena = model.Napomena,
                Zaključano = false,
            };
            _db.Add(ispit);
            _db.SaveChanges();

            IEnumerable<IspitStudent> ispitStudent = _db.SlusaPredmet.Where(x => x.AngazovanId == model.AngazovanId && (x.Ocjena==null || x.Ocjena==5)).Select(x=> new IspitStudent { 
                SlusaPredmetId=x.Id,
                IspitId=ispit.Id,
                Pristupio=false,
                Prijava=false,
                Polozio=false,
            });
            foreach (var i in ispitStudent)
            {
                _db.Add(i);
            }
            _db.SaveChanges();

            return Redirect("/Ispit/Korak2/"+ model.AngazovanId);
        }
        public IActionResult Korak4(int Id)
        {
            Korak3 viewModel = IzvuciIspit(Id);
            return View(viewModel);
        }
        public IActionResult Korak4PrikaziStudente(int Id)
        {
            IEnumerable<Korak4PrikaziStudente> viewModel = _db.IspitStudent.Where(x => x.IspitId == Id).Select(x => new Korak4PrikaziStudente
            {
                IspitStudentId = x.Id,
                Ocjena = x.Ocjena,
                Pristupio = x.Pristupio,
                Student = x.SlusaPredmet.UpisGodine.Student.Ime + " " + x.SlusaPredmet.UpisGodine.Student.Prezime,
            });
            return PartialView(viewModel);
        }
        public IActionResult Korak4Uredi(int Id)
        {
            Korak4Uredi viewModel = _db.IspitStudent.Where(x => x.Id == Id).Select(x => new Korak4Uredi 
            { 
                Ocjena=x.Ocjena,
                Student= x.SlusaPredmet.UpisGodine.Student.Ime + " " + x.SlusaPredmet.UpisGodine.Student.Prezime,
                IspitStudentId=x.Id,
            }).FirstOrDefault();

            return PartialView(viewModel);
        }
        public IActionResult Korak4UrediSnimi(Korak4Uredi model)
        {
            var student = _db.IspitStudent.Where(x => x.Id == model.IspitStudentId).FirstOrDefault();
            student.Ocjena = model.Ocjena;
            student.Polozio = true;
            student.Prijava = true;
            student.Pristupio = true;
            _db.SaveChanges();
            return Redirect("/Ispit/Korak4PrikaziStudente/"+ student.IspitId);
        }
        public IActionResult Promjena(int id)
        {
            var student = _db.IspitStudent.Where(x => x.Id == id).FirstOrDefault();
            student.Prijava = !student.Prijava;
            student.Pristupio = ! student.Pristupio;
            _db.SaveChanges();
            return Redirect("/Ispit/Korak4PrikaziStudente/" + student.IspitId);
        }
        public IActionResult Ocjena(int Id , int Ocjena)
        {
            var student = _db.IspitStudent.Where(x => x.Id == Id).FirstOrDefault();
            student.Ocjena = Ocjena;
            student.Pristupio = !student.Pristupio;
            student.Polozio = true;
            student.Prijava = true;
            _db.SaveChanges();
            return Redirect("/Ispit/Korak4PrikaziStudente/" + student.IspitId);
        }
    }
}