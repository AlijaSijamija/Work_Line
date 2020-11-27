using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class TaskPrikaziVM
    {
        public int Id { get; set; }
        public int Bodovi { get; set; }
        public string NazivTaska { get; set; }
        public string DatumVrijemeDodavanja { get; set; }
        public string RokZaIzvrsavanje { get; set; }
        public string Projekt { get; set; }
        public string Korisnik { get; set; }
        public string Vjestina { get; set; }
        public bool Zauzet { get; set; }
        public bool ProductOwner { get; set; }
    }

}
