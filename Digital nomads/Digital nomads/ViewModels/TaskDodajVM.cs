using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class TaskDodajVM
    {
        public int TaskId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Bodovi { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Rok { get; set; }
        public IEnumerable<SelectListItem> Projekti { get; set; }
        public int ProjektId { get; set; }
        public IEnumerable<SelectListItem> Vjestine { get; set; }
        public int VjestineId { get; set; }



    }
}
