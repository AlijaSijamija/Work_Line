using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Bodovi { get; set; }
        public bool Zauzet { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime? Kraj { get; set; }
        public DateTime Rok { get; set; }

        public Projekt Projekt { get; set; }
        public int ProjektId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int? KorisnikId { get; set; }
        public Vjestina Vjestina { get; set; }
        public int VjestinaID { get; set; }

    }
}
