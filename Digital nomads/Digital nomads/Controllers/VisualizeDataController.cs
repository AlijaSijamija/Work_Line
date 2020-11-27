using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Digital_nomads.Data;
using Digital_nomads.Helper;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace codemode_youtube.Controllers
{
    public class VisualizeDataController : Controller
    {
        private MojDbContext db;
        public VisualizeDataController(MojDbContext mojDbContext)
        {
            db = mojDbContext;
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
                    db.Task.Where(a => a.Kraj.Value.Month == item.Item1 && a.Kraj.Value.Year == item.Item2).Sum(a => a.Bodovi));
                monthsPlusSumeBodova.Add(local);
            }
            var sortLista = monthsPlusSumeBodova.OrderByDescending(a => a.Item3).ToList();
            myVm.BestScore = sortLista.First().Item3;
            return myVm;
        }
        public JsonResult pozicijeTop5(int brojMjeseca)
        {
            var radniciReturned = radniciPoMjesecuTop5(brojMjeseca);
            List<string> pozicije = new List<string>();
            foreach (var item in radniciReturned)
            {
                ProjektniTim local = db.ProjektniTim.Where(a => a.KorisnikID == item.Item1.Id).First();
                pozicije.Add(db.RoleNaProjektu.Where(a => a.Id == local.RolaNaProjektuId).Select(a => a.Rola).FirstOrDefault());
            }
            List<string> svePozicije = new List<string>();
            svePozicije.Add("Product Owner");
            svePozicije.Add("Frontend Developer");
            svePozicije.Add("Backend Developer");
            svePozicije.Add("Designer");
            List<Tuple<string, float>> pozicijePostotak = new List<Tuple<string, float>>();
            foreach (var item in svePozicije)
            {
                pozicijePostotak.Add(new Tuple<string, float>(item, (float)pozicije.Where(a => a == item).Count() / radniciReturned.Count));
            }
            List<StudentResult> rezultati = new List<StudentResult>();
            foreach (var item in pozicijePostotak)
            {
                rezultati.Add(new StudentResult() { stdName = item.Item1, marksObtained = item.Item2 });
            }
            return Json(rezultati);
        }
        public JsonResult radniciPoMjesecuGraph(int brojMjeseca)
        {
            List<StudentResult> listaRezultata = new List<StudentResult>();
            var allFinishedTasks = db.Task.Where(a=>a.KorisnikId.HasValue && a.Kraj.HasValue&&a.Kraj.Value.Month==brojMjeseca).ToList();
            var userIdsDistinct = allFinishedTasks.Select(a => a.KorisnikId).Distinct().ToList();
            List<Tuple<Korisnik, int>> tuples = new List<Tuple<Korisnik, int>>();
            foreach (var item in userIdsDistinct)
            {
                tuples.Add(new Tuple<Korisnik, int>(db.Korsinik.Where(a => a.Id == item).FirstOrDefault(),
                    allFinishedTasks.Where(a => a.KorisnikId == item).Sum(a => a.Bodovi)));
            }
            foreach (var item in tuples)
            {
                listaRezultata.Add(new StudentResult() { stdName = item.Item1.Ime + " " + item.Item1.Prezime, marksObtained = item.Item2 });
            }
            listaRezultata.OrderByDescending(a => a.marksObtained);
            return Json(listaRezultata);
        }
        public List<Tuple<Korisnik,int>> radniciPoMjesecuTop5(int brojMjeseca)
        {
            var allFinishedTasks = db.Task.Where(a => a.KorisnikId.HasValue && a.Kraj.HasValue && a.Kraj.Value.Month == brojMjeseca).ToList();
            var userIdsDistinct = allFinishedTasks.Select(a => a.KorisnikId).Distinct().ToList();
            List<Tuple<Korisnik, int>> tuples = new List<Tuple<Korisnik, int>>();
            foreach (var item in userIdsDistinct)
            {
                tuples.Add(new Tuple<Korisnik, int>(db.Korsinik.Where(a => a.Id == item).FirstOrDefault(),
                    allFinishedTasks.Where(a => a.KorisnikId == item).Sum(a => a.Bodovi)));
            }
            var returnTuples = tuples.OrderByDescending(a => a.Item2).Take(5).ToList();
            return returnTuples;
        }
        public ActionResult ColumnChart()
        {
            return View();
        }
     
        public ActionResult PieChart()
        {
            return View();
        }
 
        public ActionResult LineChart()
        {
            return View();
        }
         
        public JsonResult VisualizeStudentResult()
        { 
            return Json(Result());
        }
         
        public List<StudentResult> Result()
        {
            List<StudentResult> stdResult = new List<StudentResult>();

            stdResult.Add(new StudentResult()
            {
                stdName = "Anisa",
                marksObtained = 88
            });
            stdResult.Add(new StudentResult()
            {
                stdName = "Alija",
                marksObtained = 60
            });
            stdResult.Add(new StudentResult()
            {
                stdName = "Ajdin",
                marksObtained = 77
            });
            stdResult.Add(new StudentResult()
            {
                stdName = "Kenan",
                marksObtained = 80
            });
            stdResult.Add(new StudentResult()
            {
                stdName = "Ajna",
                marksObtained = 95
            });
            stdResult.Add(new StudentResult()
            {
                stdName = "Rijad",
                marksObtained = 90
            });
            return stdResult;
        }
    }
}