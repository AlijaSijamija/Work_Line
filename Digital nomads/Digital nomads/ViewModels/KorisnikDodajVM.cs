using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class KorisnikDodajVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public List<SelectListItem> LoginRola { get; set; }
        public int LoginRolaId { get; set; }
        public int LoginID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IFormFile Photo { get; set; }
        public int PhotoRutaId { get; set; }
        public string PhotoRuta { get; set; }
        public List<SelectListItem> PhotoRoutes { get; set; }
        public IEnumerable<SelectListItem> Vjestine { get; set; }
        public List<int> VjestineId { get; set; }

    }
}
