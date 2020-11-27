using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class VjestinaKorisnik
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        public Vjestina Vjestina { get; set; }
        public int VjestinaID { get; set; }

    }
}
