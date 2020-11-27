using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Digital_nomads.Models;
using Digital_nomads.Data;
using Digital_nomads.ViewModels;
using Digital_nomads.Helper;

namespace Digital_nomads.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MojDbContext _db;
        public HomeController(ILogger<HomeController> logger,MojDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("Login", "");
        }
        LogiraniKorisnikSesija napraviLogiranogKorisnika(Korisnik k)
        {
            LogiraniKorisnikSesija lSend = new LogiraniKorisnikSesija()
            {
                DatumZaposlenja = k.DatumZaposlenja,
                Id = k.Id,
                Ime = k.Ime,
                LoginID = k.LoginID,
                Login = _db.Login.Where(a=>a.Id==k.LoginID).FirstOrDefault(),
                LoginRola = _db.LoginRola.Where(a=>a.Id==k.LoginRolaId).FirstOrDefault(),
                LoginRolaId = k.LoginRolaId,
                PhotoRuta = k.PhotoRuta,
                Prezime = k.Prezime
            };
            return lSend;
        }
        public IActionResult VerifikacijaLogina(string username,string password)
        {
            var foundUser = _db.Login.Where(a => a.Password == password && a.UserName == username).FirstOrDefault();
            if (foundUser==null)
            {
                return View("Login", "Nevalidni kredencijali!");
            }
            else
            {
                Korisnik korisnik = _db.Korsinik.Where(a => a.LoginID == foundUser.Id).FirstOrDefault();
                HttpContext.SetLogiraniKorisnik(napraviLogiranogKorisnika(korisnik), false);
                if (korisnik.isZaposlen)
                {
                    if (korisnik.LoginRola.OpisRole == "Administrator")
                        return Redirect("/Administrator/Index");
                    else
                        return Redirect("/Radnik/Index");
                }
                else
                {
                    HttpContext.SetLogiraniKorisnik(null);
                    return View("Login", "Niste zaposleni!");
                }
                
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
