using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_nomads.Data;
using Digital_nomads.ViewModels;
using eDnevnik.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digital_nomads.Controllers
{
    public class KorisnikController : Controller
    {
        private MojDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public KorisnikController(MojDbContext dbContext, IWebHostEnvironment _hostingEnvironment)
        {
            _db = dbContext;
            this._hostingEnvironment = _hostingEnvironment;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Prikazi()
        {
            List<KorisnikPrikaziVM> model = _db.Korsinik.Select(x => new KorisnikPrikaziVM
            {
                Id = x.Id,
                DatumZaposlenja = x.DatumZaposlenja,
                Ime = x.Ime,
                LoginID = x.LoginID,
                LoginRolaId = x.LoginRolaId,
                // PhotoRuta = ".." + FileUploadDelete.GetRoute(x.PhotoRuta),
                PhotoRuta = x.PhotoRuta,
                Prezime = x.Prezime,
                isZaposlen = x.isZaposlen,
                Skilovi = _db.VjestinaKorisnik.Where(s=> s.KorisnikID==x.Id).Select(s=>s.Vjestina.Naziv).ToList()
            }).ToList();
            return View(model);
        }
        public IActionResult DodajKorisnika()
        {
            //Treba dodati za sliku
            KorisnikDodajVM model = new KorisnikDodajVM
            {
                LoginRola = _db.LoginRola.Select(x => new SelectListItem
                {
                    Text = x.OpisRole,
                    Value = x.Id.ToString()
                }).ToList(),
                PhotoRoutes = _db.Photos.Select(x=> new SelectListItem
                {
                    Text = x.PhotoRoute,
                    Value = x.Id.ToString()
                }).ToList(),
                Vjestine = _db.Vjestina.Where(x=> !x.Disabled).Select(x=> new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList(),
            };
            ViewBag.Vjestine = new MultiSelectList(model.Vjestine, "Value", "Text");
            //alija, ova prazna poruka je ako još korisnik nije dodan,  
            //a u slučaju da bude neuspješna dodaj poruku neuspješno dodavanje
            return View(model);
        }
        public IActionResult PromjeniZaposlenFlag(int Id)
        {
            bool local = _db.Korsinik.Where(a => a.Id == Id).FirstOrDefault().isZaposlen;
            var lik = _db.Korsinik.Where(a => a.Id == Id).FirstOrDefault();
            if (local==false)
            {
                lik.isZaposlen = true;
            }
            else
            {
                lik.isZaposlen = false;
            }
            _db.SaveChanges();
            return Redirect("/Korisnik/Prikazi");

        }
        public IActionResult SnimiKorisnika(KorisnikDodajVM vm)
        {
            if (vm.Id>0)
            {
                var foundLogin = _db.Login.Where(a => a.Id == vm.LoginID).FirstOrDefault();
                foundLogin.UserName = vm.Username; foundLogin.Password = vm.Password;
                _db.SaveChanges();
                var foundUser = _db.Korsinik.Where(a => a.Id == vm.Id).FirstOrDefault();
                foundUser.Ime = vm.Ime; foundUser.LoginID = vm.LoginID; foundUser.LoginRolaId = vm.LoginRolaId;
                foundUser.PhotoRuta = _db.Photos.Where(a=>a.Id==vm.PhotoRutaId).Select(s=>s.PhotoRoute).FirstOrDefault(); foundUser.DatumZaposlenja = vm.DatumZaposlenja; foundUser.Prezime = vm.Prezime;
                _db.VjestinaKorisnik.RemoveRange(_db.VjestinaKorisnik.Where(a => a.KorisnikID == vm.Id).ToList());
                _db.SaveChanges();
                foreach (var item in vm.VjestineId)
                {
                    Models.VjestinaKorisnik local = new Models.VjestinaKorisnik() { KorisnikID = vm.Id, VjestinaID = item };
                    _db.VjestinaKorisnik.Add(local);
                }
                _db.SaveChanges();
                return Redirect("/Korisnik/Prikazi");
            }
            else
            {
                Models.Login localLogin = new Models.Login() { Password = vm.Password, UserName = vm.Username };
                _db.Login.Add(localLogin);
                _db.SaveChanges();
                Models.Korisnik localKorisnik = new Models.Korisnik()
                {
                    Ime = vm.Ime,
                    DatumZaposlenja = vm.DatumZaposlenja,
                    LoginID = localLogin.Id,
                    LoginRolaId = vm.LoginRolaId,
                    //PhotoRuta = FileUploadDelete.Upload(_hostingEnvironment, vm.Photo, "imgUpload"),
                    PhotoRuta = _db.Photos.Where(a => a.Id == vm.PhotoRutaId).Select(s => s.PhotoRoute).FirstOrDefault(),
                    Prezime = vm.Prezime,
                    isZaposlen = true
                };
                _db.Korsinik.Add(localKorisnik);
                _db.SaveChanges();
                foreach (var item in vm.VjestineId)
                {
                    Models.VjestinaKorisnik local = new Models.VjestinaKorisnik() { KorisnikID = localKorisnik.Id, VjestinaID = item };
                    _db.VjestinaKorisnik.Add(local);
                }
                _db.SaveChanges();
                return Redirect("/Korisnik/Prikazi");
            }
        }
        public IActionResult Izmjeni(int Id)
        {
            KorisnikDodajVM novi = _db.Korsinik.Where(x => x.Id == Id).Select(x => new KorisnikDodajVM
            {
                Id = x.Id,
                DatumZaposlenja = x.DatumZaposlenja,
                Ime = x.Ime,
                LoginID = x.LoginID,
                LoginRolaId = x.LoginRolaId,
                Password = x.Login.Password,
                PhotoRuta = x.PhotoRuta,
                Prezime = x.Prezime,
                Username = x.Login.UserName,
                LoginRola = _db.LoginRola.Select(x => new SelectListItem
                {
                    Text = x.OpisRole,
                    Value = x.Id.ToString()
                }).ToList(),
                PhotoRoutes = _db.Photos.Select(x => new SelectListItem
                {
                    Text = x.PhotoRoute,
                    Value = x.Id.ToString()
                }).ToList(),
                Vjestine = _db.Vjestina.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList(),
            }).FirstOrDefault();
            return View(novi);
        }
    }
}
