using Digital_nomads.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class KorisnikPrikaziVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public int LoginRolaId { get; set; }
        public int LoginID { get; set; }
        public string PhotoRuta { get; set; }
        public bool isZaposlen { get; set; }
        public List<string> Skilovi { get; set; }
    }
}
