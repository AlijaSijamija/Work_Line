using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using codemode_youtube.Controllers;
using Digital_nomads.Data;
using Digital_nomads.Helper;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Digital_nomads.Controllers
{
    public class RadnikController : Controller
    {
        private MojDbContext db;

        public RadnikController(MojDbContext mojDbContext)
        {
            db = mojDbContext;
        }
        public IActionResult Profil()
        {
            RadnikProfilVM myVm = new RadnikProfilVM();
            myVm.NameAndSurname = HttpContext.GetLogiraniKorisnik().Ime + " " + HttpContext.GetLogiraniKorisnik().Prezime;
            var getFirstProjektRole = db.ProjektniTim.Where(a => a.KorisnikID == HttpContext.GetLogiraniKorisnik().Id).FirstOrDefault();
            myVm.JobDescription = db.RoleNaProjektu.Where(a => a.Id == getFirstProjektRole.RolaNaProjektuId).Select(a => a.Rola).FirstOrDefault();
            var getProjekti = db.ProjektniTim.Where(a => a.KorisnikID == HttpContext.GetLogiraniKorisnik().Id).ToList();
            List<Projekt> listaProjekata = new List<Projekt>();
            foreach (var item in getProjekti)
            {
                listaProjekata.Add(db.Projekt.Where(a => a.Id == item.ProjektId).FirstOrDefault());
            }
            var sortedList = listaProjekata.OrderByDescending(a => a.DatumPocetka).ToList();
            myVm.DateOfLastProject = (DateTime)sortedList.Select(a => a.DatumPocetka).First();
            myVm.Id = HttpContext.GetLogiraniKorisnik().Id;
            myVm.NumberOfProjects = getProjekti.Count;
            myVm.NumberOfTrophies = db.TrofejKorisnik.Where(a => a.KorisnikId == myVm.Id).Count();
            return View(myVm);
        }
        public PersonalStatisticUserVM GetPersonalStatistic()
        {
            var loggedUser = HttpContext.GetLogiraniKorisnik();
            var projectUser = db.ProjektniTim.Where(a => a.KorisnikID == loggedUser.Id).FirstOrDefault();
            PersonalStatisticUserVM myVm = new PersonalStatisticUserVM();
            myVm.Position = db.RoleNaProjektu.Where(a => a.Id == projectUser.RolaNaProjektuId).Select(a => a.Rola).FirstOrDefault();
            myVm.NumberOfTrophies = db.TrofejKorisnik.Where(a => a.KorisnikId == loggedUser.Id).Count();
            myVm.Points = db.Task.Where(a => a.KorisnikId == loggedUser.Id && a.Kraj.HasValue).Sum(a => a.Bodovi);
            //mjesec, godina
            List<Tuple<int, int>> months = new List<Tuple<int, int>>();
            var allUserTasks = db.Task.Where(a => a.KorisnikId == loggedUser.Id && a.Kraj.HasValue).ToList();
            foreach (var item in allUserTasks)
            {
                months.Add(new Tuple<int, int>(item.Kraj.Value.Month, item.Kraj.Value.Year));
            }
            List<Tuple<int, int>> distinctMonths = months.Distinct().ToList();
            List<Tuple<int, int, int>> monthsPlusSumeBodova = new List<Tuple<int, int, int>>();
            foreach (var item in distinctMonths)
            {
                Tuple<int, int, int> local = new Tuple<int, int, int>(item.Item1, item.Item2,
                    db.Task.Where(a => a.Kraj.Value.Month == item.Item1 && a.Kraj.Value.Year == item.Item2 && a.KorisnikId==loggedUser.Id).Sum(a => a.Bodovi));
                monthsPlusSumeBodova.Add(local);
            }
            var sortLista = monthsPlusSumeBodova.OrderByDescending(a => a.Item3).ToList();
            if (sortLista.Count != 0)
            {
                myVm.BestScore = sortLista.First().Item3;
            }
            else myVm.BestScore = 0;
            return myVm;
        }
        public IActionResult Index()
        {
            var allFinishedTasks = db.Task.Where(a => a.KorisnikId.HasValue && a.Kraj.HasValue && a.Kraj.Value.Month == DateTime.Now.Month).ToList();
            var userIdsDistinct = allFinishedTasks.Select(a => a.KorisnikId).Distinct().ToList();
            List<Tuple<Korisnik, int>> tuples = new List<Tuple<Korisnik, int>>();
            foreach (var item in userIdsDistinct)
            {
                tuples.Add(new Tuple<Korisnik, int>(db.Korsinik.Where(a => a.Id == item).FirstOrDefault(),
                    allFinishedTasks.Where(a => a.KorisnikId == item).Sum(a => a.Bodovi)));
            }
            var returnTuples = tuples.OrderByDescending(a => a.Item2).Take(5).ToList();
            var personalStats = GetPersonalStatistic();
            PersonalStatsTopUsersVM myVm = new PersonalStatsTopUsersVM() {name = HttpContext.GetLogiraniKorisnik().Ime + " "+HttpContext.GetLogiraniKorisnik().Prezime, personalStats = personalStats, tuples = returnTuples };
            return View(myVm);
        }
        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return Redirect("/Home/Login");
        }

    }
}
