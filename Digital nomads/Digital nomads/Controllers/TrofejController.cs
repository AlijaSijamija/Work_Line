using Digital_nomads.Data;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Controllers
{
    public class TrofejController : Controller
    {
        private readonly MojDbContext _db;
        public TrofejController( MojDbContext db)
        {
            _db = db;
        }
        public IActionResult Prikazi()
        {
            List<TrofejPrikaziVM> model = _db.Trofej.Select(x => new TrofejPrikaziVM
            {
                Id = x.Id,
                NazivTrofeja = x.NazivTrofeja
            }).ToList();
            return View(model);
        }
        public IActionResult Award()
        {
            if (_db.TrofejKorisnik.Where(a=>a.DatumDodjele.Month==DateTime.Now.Month).FirstOrDefault() != null)
            {
                return RedirectToAction("Prikazi");
            }
            var getFinishedTasks = _db.Task.Where(a => a.KorisnikId.HasValue && a.Kraj.HasValue).ToList();
            var distinctUserIds = getFinishedTasks.Select(a => a.KorisnikId).ToList().Distinct();
            //korisnik, suma bodova
            List<Tuple<int, int>> tuples = new List<Tuple<int, int>>();
            foreach (var item in distinctUserIds)
            {
                tuples.Add(new Tuple<int, int>((int)item, getFinishedTasks.Where(a => a.KorisnikId == item).Sum(a => a.Bodovi)));
            }
            var sortedTuples = tuples.OrderByDescending(a => a.Item2).ToList();

            int firstplacetrophyId = _db.Trofej.Where(a => a.NazivTrofeja == "1st Place").Select(a=>a.Id).FirstOrDefault();
            int secondplacetrophyId = _db.Trofej.Where(a => a.NazivTrofeja == "2nd Place").Select(a => a.Id).FirstOrDefault();
            int thirdplacetrophyId = _db.Trofej.Where(a => a.NazivTrofeja == "3rd Place").Select(a => a.Id).FirstOrDefault();

            if (tuples.Count>=3)
            {
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(0).Item1, TrofejId = firstplacetrophyId, DatumDodjele=DateTime.Now });
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(1).Item1, TrofejId = secondplacetrophyId, DatumDodjele = DateTime.Now });
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(2).Item1, TrofejId = thirdplacetrophyId, DatumDodjele = DateTime.Now });
                _db.SaveChanges();
            }
            else if (tuples.Count==2)
            {
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(0).Item1, TrofejId = firstplacetrophyId, DatumDodjele = DateTime.Now });
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(1).Item1, TrofejId = secondplacetrophyId, DatumDodjele = DateTime.Now });
                _db.SaveChanges();
            }
            else if (tuples.Count==1)
            {
                _db.TrofejKorisnik.Add(new TrofejKorisnik() { KorisnikId = tuples.ElementAt(0).Item1, TrofejId = firstplacetrophyId, DatumDodjele = DateTime.Now });
                _db.SaveChanges();
            }
            return RedirectToAction("Prikazi");
        }
    }
}
