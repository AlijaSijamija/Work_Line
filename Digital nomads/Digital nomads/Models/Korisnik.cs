using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class Korisnik
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
        public bool isZaposlen { get; set; }

    }
}
