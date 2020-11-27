using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class TrofejKorisnik
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public Trofej Trofej { get; set; }
        public int TrofejId { get; set; }
        public DateTime DatumDodjele { get; set; }



    }
}
