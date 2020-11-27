using Digital_nomads.Data;
using Digital_nomads.Models;
using Digital_nomads.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Controllers
{
    public class VjestineController : Controller
    {
        private MojDbContext _db;
        public VjestineController(MojDbContext db)
        {
            _db = db;
        }
        public IActionResult Prikazi()
        {
            IEnumerable<VjestinaVM> model = _db.Vjestina.Where(x=> !x.Disabled).Select(x => new VjestinaVM
            {
                Naziv = x.Naziv,
                Id = x.Id
            }).OrderBy(x=> x.Naziv);
            return View(model);
        }
        public IActionResult VjestineDodaj(VjestinaVM model=null)
        {
            if(model!=null)
                return View("VjestineDodaj",model);
            return View("VjestineDodaj");
        }
        public IActionResult VjestinaSnimi(VjestinaVM model)
        {
            if (model.Id>0)
            {
                _db.Vjestina.Where(x => x.Id == model.Id).FirstOrDefault().Naziv = model.Naziv;
                _db.SaveChanges();
                return RedirectToAction("Prikazi");
            }
            _db.Add(new Vjestina {Naziv=model.Naziv, Disabled=false});
            _db.SaveChanges();
            return RedirectToAction("Prikazi");
        }
        public IActionResult VjestineIzmjeni(int Id)
        {
            VjestinaVM model = _db.Vjestina.Where(x => x.Id == Id).Select(x => new VjestinaVM {
                Naziv=x.Naziv,
                Id=x.Id,                
            }).FirstOrDefault();

            return View("VjestineDodaj",model);
        }

        public IActionResult VjestineObrisi(int Id)
        {
            _db.Vjestina.Where(x => x.Id == Id).FirstOrDefault().Disabled = true;
            _db.SaveChanges();
            return RedirectToAction("Prikazi");
        }



    }
}
