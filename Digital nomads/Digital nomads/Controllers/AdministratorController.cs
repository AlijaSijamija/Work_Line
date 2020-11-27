using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Digital_nomads.Data;
using Digital_nomads.Helper;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Digital_nomads.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly MojDbContext _db;
        public AdministratorController(MojDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            var allFinishedTasks = _db.Task.Where(a => a.KorisnikId.HasValue && a.Kraj.HasValue && a.Kraj.Value.Month == DateTime.Now.Month).ToList();
            var userIdsDistinct = allFinishedTasks.Select(a => a.KorisnikId).Distinct().ToList();
            List<Tuple<Korisnik, int>> tuples = new List<Tuple<Korisnik, int>>();
            foreach (var item in userIdsDistinct)
            {
                tuples.Add(new Tuple<Korisnik, int>(_db.Korsinik.Where(a => a.Id == item).FirstOrDefault(),
                    allFinishedTasks.Where(a => a.KorisnikId == item).Sum(a => a.Bodovi)));
            }
            var returnTuples = tuples.OrderByDescending(a => a.Item2).Take(5).ToList();
            TopUsersVM myVm = new TopUsersVM() { name = HttpContext.GetLogiraniKorisnik().Ime + " " + HttpContext.GetLogiraniKorisnik().Prezime, tuples = returnTuples };
            myVm.Projekti = _db.Projekt.Select(s => s.NazivProjekta).ToList();
            return View(myVm);
        }
        public IActionResult Profil()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return Redirect("/Home/Login");
        }
        public IActionResult Vjestine()
        {
            return Redirect("/Vjestine/Prikazi");
        }
        public IActionResult Trofej()
        {
            return Redirect("/Trofej/Prikazi");
        }
        public IActionResult Korisnik()
        {
            return Redirect("/Korisnik/Prikazi");
        }
        public IActionResult Projekti()
        {
            return Redirect("/Projekt/Index");
        }
        
    }
}