using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class ProjektDodajVM
    {
        public int ProjektId { get; set; }
        public string NazivProjekta { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime Rok { get; set; }
        public string Opis { get; set; }
        public IEnumerable<SelectListItem> listaRadnika { get; set; }
        public int RadnikId { get; set; }
    }
}
