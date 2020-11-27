using Digital_nomads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class LogiraniKorisnikSesija
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public LoginRola LoginRola { get; set; }
        public int LoginRolaId { get; set; }
        public Login Login { get; set; }
        public int LoginID { get; set; }
        public string PhotoRuta { get; set; }
    }
}
