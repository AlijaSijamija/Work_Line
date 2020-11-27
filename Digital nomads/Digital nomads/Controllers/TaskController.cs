using Digital_nomads.Data;
using Digital_nomads.Helper;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Controllers
{
    public class TaskController : Controller
    {
        private MojDbContext _db;
        public TaskController(MojDbContext db)
        {
            _db = db;
        }
        public IActionResult TaskDodaj(TaskDodajVM pocetni = null)
        {
            TaskDodajVM model = new TaskDodajVM
            {
                Projekti = _db.Projekt.Where(x=> x.DatumZavrsetka==null).Select(x => new SelectListItem
                {
                    Text = x.NazivProjekta,
                    Value = x.Id.ToString(),
                }),
                Vjestine = _db.Vjestina.Where(x=> !x.Disabled).Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString(),
                }),
                Pocetak = DateTime.Now,
            };
            if (pocetni == null)
                return View(model);
            model.TaskId = pocetni.TaskId;
            model.Bodovi = pocetni.Bodovi;
            model.Opis = pocetni.Opis;
            model.Pocetak = pocetni.Pocetak;
            model.ProjektId = pocetni.ProjektId;
            model.Rok = pocetni.Rok;
            model.VjestineId = pocetni.VjestineId;
            model.Naziv = pocetni.Naziv;
            return View(model);
        }
        public IActionResult TaskPrikazi()
        {

            List< TaskPrikaziVM> model = new List<TaskPrikaziVM>();
            Korisnik korisnik = _db.Korsinik.Where(x => x.LoginID == HttpContext.GetLogiraniKorisnik().LoginID).FirstOrDefault();
            string rola1 = _db.ProjektniTim.Include(x => x.RolaNaProjektu).Where(x => x.KorisnikID == korisnik.Id && x.Projekt.DatumZavrsetka == null).Select(x => x.RolaNaProjektu.Rola).FirstOrDefault();
           //admin
            if (string.Compare(_db.LoginRola.Where(x=> x.Id==korisnik.LoginRolaId).Select(x=> x.OpisRole).FirstOrDefault(), "Administrator") == 0)
            {
                model = _db.Task.Include(x=> x.Vjestina).Select(x => new TaskPrikaziVM
                {
                    Bodovi=x.Bodovi,
                    DatumVrijemeDodavanja=x.Pocetak.ToString(),
                    NazivTaska=x.Naziv,
                    RokZaIzvrsavanje=x.Rok.ToString(),
                    Vjestina=x.Vjestina.Naziv,
                    ProductOwner=true
                }).ToList();
                if (model.Count() == 0)
                {
                    model.Add(new TaskPrikaziVM { ProductOwner = true });
                }
                return View(model);

            }
            //product owner
            if (string.Compare(rola1, "Product Owner")==0)
            {
                foreach (var i in _db.Task.Include(x=> x.Vjestina).Include(x=> x.Projekt).Where(x => x.ProjektId== _db.ProjektniTim.Where(x => x.KorisnikID == korisnik.Id && x.Projekt.DatumZavrsetka == null).FirstOrDefault().ProjektId))
                {
                    model.Add(new TaskPrikaziVM
                    {
                        Id = i.Id,
                        Bodovi = i.Bodovi,
                        Projekt = i.Projekt.NazivProjekta,
                        NazivTaska = i.Naziv,
                        RokZaIzvrsavanje = i.Rok.ToString(),
                        Vjestina = i.Vjestina.Naziv,
                        DatumVrijemeDodavanja = i.Pocetak.ToString(),
                        ProductOwner = true
                    });
                }
                if (model.Count() == 0)

                {
                    model.Add(new TaskPrikaziVM { ProductOwner = true });
                }
                return View(model);
            }

            //preuzeti projekt

            Models.Task task = _db.Task.Include(x=> x.Vjestina).Include(x=> x.Projekt).Where(x => x.KorisnikId == korisnik.Id && x.Kraj == null).FirstOrDefault();
            if (task!=null)
            {
                model.Add(new TaskPrikaziVM
                {
                    Id = task.Id,
                    Bodovi = task.Bodovi,
                    Projekt = task.Projekt.NazivProjekta,
                    NazivTaska = task.Naziv,
                    RokZaIzvrsavanje = task.Rok.ToString(),
                    Vjestina = task.Vjestina.Naziv,
                    DatumVrijemeDodavanja = task.Pocetak.ToString(),
                    Zauzet=task.Zauzet,
                    ProductOwner=false
                });
                if (model.Count() == 0)

                {
                    model.Add(new TaskPrikaziVM { ProductOwner = false });
                }
                return View(model);
            }
            // ponuđeni projektnom timu
            foreach (var i in _db.Task.Include(x=> x.Projekt).Include(x=> x.Vjestina).Where(x=> x.KorisnikId==null && x.ProjektId == _db.ProjektniTim.Where(x => x.KorisnikID == korisnik.Id && x.Projekt.DatumZavrsetka == null).FirstOrDefault().ProjektId))//pprovjetiri
            {
                foreach (var x in _db.VjestinaKorisnik.Where(x=> x.KorisnikID==korisnik.Id))
                {
                    if (i.VjestinaID==x.VjestinaID)
                    {
                        model.Add(new TaskPrikaziVM
                        {
                            Id = i.Id,
                            Bodovi=i.Bodovi,
                            Projekt=i.Projekt.NazivProjekta,
                            NazivTaska=i.Naziv,
                            RokZaIzvrsavanje=i.Rok.ToString(),
                           Vjestina=i.Vjestina.Naziv,
                           DatumVrijemeDodavanja=i.Pocetak.ToString(),
                           ProductOwner=false
                        });
                    }
                }
            }
            if (model.Count()==0)
            {
                model.Add(new TaskPrikaziVM { ProductOwner = false });
            }
            return View(model);
        }

        public IActionResult TaskUredi(int Id)
        {
            TaskDodajVM model = _db.Task.Where(x => x.Id == Id).Select(x => new TaskDodajVM
            {
                Bodovi = x.Bodovi,
                Opis = x.Opis,
                Pocetak = x.Pocetak,
                Rok = x.Rok,
                ProjektId = x.ProjektId,
                VjestineId = x.VjestinaID,
                TaskId = x.Id,
                Naziv=x.Naziv,
            }).FirstOrDefault();
            return RedirectToAction("TaskDodaj",model);
        }

        public IActionResult TaskIzaberi(int Id)
        {
            Korisnik korisnik = _db.Korsinik.Where(x => x.LoginID == HttpContext.GetLogiraniKorisnik().LoginID).FirstOrDefault();
            _db.Task.Where(x => x.Id == Id).FirstOrDefault().KorisnikId = korisnik.Id;
            _db.Task.Where(x => x.Id == Id).FirstOrDefault().Zauzet = true;
            _db.SaveChanges();
            return RedirectToAction("TaskPrikazi");
        }

        public IActionResult Snimi(TaskDodajVM model)
        {
            if (model.TaskId==0)
            {
                _db.Add(new Digital_nomads.Models.Task
                {
                     Naziv=model.Naziv,
                     Bodovi=model.Bodovi,
                     Rok=model.Rok,
                     Opis=model.Opis,
                     VjestinaID=model.VjestineId,
                     ProjektId=model.ProjektId,
                     Pocetak=model.Pocetak,
                });
                _db.SaveChanges();
                return RedirectToAction("TaskPrikazi");
            }
            Digital_nomads.Models.Task task = _db.Task.Where(x => x.Id == model.TaskId).FirstOrDefault();
            task.Naziv = model.Naziv;
            task.Bodovi = model.Bodovi;
            task.Rok = model.Rok;
            task.Opis = model.Opis;
            task.VjestinaID = model.VjestineId;
            task.ProjektId = model.ProjektId;
            task.Pocetak = model.Pocetak;

            _db.SaveChanges();

            return RedirectToAction("TaskPrikazi");
        }

        public IActionResult TaskZavrsi (int Id)
        {
            Models.Task task = _db.Task.Where(x => x.Id == Id).FirstOrDefault();
            task.Kraj = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("TaskPrikazi");
        }

    }


}
