using Digital_nomads.Data;
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
    public class ProjektController : Controller
    {
        private MojDbContext _db;
        public ProjektController (MojDbContext db)
        {
            _db = db;
        }
        public IActionResult PrikaziAktivneProjekte()
        {
            IEnumerable<ProjektPrikaziVM> model = _db.Projekt.Where(x => x.DatumZavrsetka == null).Select(x => new ProjektPrikaziVM
            {
                Id = x.Id,
                DatumPocetka = x.DatumPocetka.ToShortDateString(),
                NazivProjekta = x.NazivProjekta,
                Opis = x.Opis,
                Rok = x.Rok.ToShortDateString()

            });
            return View(model);
        }
        public IActionResult PrikaziZavrseneProjekte()
        {
            IEnumerable<ProjektPrikaziVM> model = _db.Projekt.Where(x => x.DatumZavrsetka != null).Select(x => new ProjektPrikaziVM
            {
                Id = x.Id,
                DatumPocetka = x.DatumPocetka.ToString(),
                NazivProjekta = x.NazivProjekta,
                Opis = x.Opis,
                Rok = x.Rok.ToString(),
                DatumZavrsetka = x.DatumZavrsetka.ToString()
            });
            return View(model);
        } 
        string vjestineToString(int radnikId)
        {
            string a="";
            int mojRadnikId = radnikId;
            var vjestineReturned = _db.VjestinaKorisnik.Where(a => a.KorisnikID == mojRadnikId).ToList();
            foreach (var item in vjestineReturned)
            {
                a += _db.Vjestina.Where(a => a.Id == item.VjestinaID).Select(a => a.Naziv).FirstOrDefault();
                a += " / ";
            }
            string b = "";
            if (a!="")
            {
                b = a.Substring(0, a.Length - 2);
            }
            return b;
        }
        public IActionResult ProjektDodaj(ProjektDodajVM model=null)
        {
            if (model.RadnikId != 0)
            {
                model.listaRadnika = _db.Korsinik.Select(y => new SelectListItem
                {
                    Text = y.Ime + ' ' + y.Prezime,
                    Value = y.Id.ToString()
                });
                return View(model);
            }

            ProjektDodajVM owner = new ProjektDodajVM
            {
                listaRadnika = _db.Korsinik.Select(a => new SelectListItem
                {
                    Text = a.Ime + " " + a.Prezime,
                    Value = a.Id.ToString()
                })
            }; 

            return View(owner);
        }
        public IActionResult ProjektUredi(int Id)
        {
            ProjektDodajVM model = _db.Projekt.Where(x => x.Id == Id).Select(x => new ProjektDodajVM
            {
                DatumPocetka = x.DatumPocetka,
                NazivProjekta=x.NazivProjekta,
                Opis=x.Opis,
                ProjektId=x.Id,
                Rok=x.Rok,
                RadnikId =_db.ProjektniTim.Include(y=> y.RolaNaProjektu).Where(y=> y.ProjektId==x.Id && string.Compare( y.RolaNaProjektu.Rola,"Product Owner")==0).Select(x=> x.KorisnikID).FirstOrDefault(),
           }).FirstOrDefault();
            return RedirectToAction("ProjektDodaj",model);
        }
        public IActionResult ProjektSnimi(ProjektDodajVM model)
        {
            if (model.ProjektId==0)
            {
                Projekt projekt = new Projekt
                {
                    DatumPocetka = DateTime.Now,
                    Rok = model.Rok,
                    NazivProjekta = model.NazivProjekta,
                    Opis = model.Opis
                };
                _db.Add(projekt);
                _db.SaveChanges();

                _db.Add(new ProjektniTim
                {
                    KorisnikID = model.RadnikId,
                    ProjektId = projekt.Id,
                    RolaNaProjektuId = _db.RoleNaProjektu.Where(x => string.Compare(x.Rola, "Product Owner") == 0).FirstOrDefault().Id
                });
                
                _db.SaveChanges();
                return RedirectToAction("PrikaziAktivneProjekte");
            }
            Projekt izmejna = _db.Projekt.Where(x => x.Id == model.ProjektId).FirstOrDefault();
            izmejna.NazivProjekta = model.NazivProjekta;
            izmejna.Opis = model.Opis;
            izmejna.Rok = model.Rok;
            izmejna.DatumPocetka = model.DatumPocetka;
            ProjektniTim owner = _db.ProjektniTim.Where(x => x.ProjektId == izmejna.Id ).FirstOrDefault();//&& string.Compare(x.Korisnik.LoginRola.OpisRole, "Product Owner") == 0
            owner.KorisnikID = model.RadnikId;
            _db.SaveChanges();
            return RedirectToAction("PrikaziAktivneProjekte");
        }

        public IActionResult ZavrsiProjekt(int Id)
        {
            _db.Projekt.Where(x => x.Id == Id).FirstOrDefault().DatumZavrsetka = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("PrikaziZavrseneProjekte");
        }
        public IActionResult ProjektDodajUcesnike(int Id)
        {
            ProjektUcesniciVM model = new ProjektUcesniciVM
            {
                ProjektId = Id,
                rows = _db.Korsinik.Except(_db.ProjektniTim.Where(x => x.Projekt.DatumZavrsetka == null).Select(x => x.Korisnik)).ToList().Select(x => new ProjektUcesniciVM.row
                {
                    Id = x.Id,
                    ImeIPrezime = x.Ime + " " + x.Prezime,
                    vjestine = _db.VjestinaKorisnik.Include(y=> y.Vjestina).Where(y => y.KorisnikID == x.Id).Select(y => y.Vjestina.Naziv),
                    Role = _db.RoleNaProjektu.Where(y=> string.Compare(y.Rola,"Product Owner")!=0).Select(s => new SelectListItem
                    {
                        Text = s.Rola,
                        Value = s.Id.ToString()
                    })
                })
            };
            return View(model);
        }
        public IActionResult ProjektDodajUcesnikeSnimi(int ProjektId, int RolaId, int UcesnikId )
        {
            
                _db.Add(new ProjektniTim
                {
                    KorisnikID = UcesnikId,
                    ProjektId = ProjektId,
                    RolaNaProjektuId = RolaId
                });
            _db.SaveChanges();
            return RedirectToAction("ProjektDodajUcesnike", new { Id = ProjektId });
        }
        public IActionResult PrikaziUcesnikeNaProjektu(int Id)
        {
            ProjektUcesniciVM model = new ProjektUcesniciVM
            {
                ProjektId = Id,
                rows = _db.ProjektniTim.Where(x=> x.ProjektId==Id).Select(x => new ProjektUcesniciVM.row
                {
                    Id = x.Id,
                    ImeIPrezime = x.Korisnik.Ime + " " + x.Korisnik.Prezime,
                    vjestine = _db.VjestinaKorisnik.Where(x => x.KorisnikID == x.Id).Select(x => x.Vjestina.Naziv)
                })
            };
            return View(model);
        }



    }
}
