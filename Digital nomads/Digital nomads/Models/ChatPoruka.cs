using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class ChatPoruka
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public Projekt Projekt { get; set; }
        public int ProjektId { get; set; }
        public bool Pin { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime VrijemePoruke { get; set; }


    }
}
