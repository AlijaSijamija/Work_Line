using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class Projekt
    {
        public int Id { get; set; }
        public string NazivProjekta { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }
        public DateTime Rok { get; set; }
        public string Opis { get; set; }


    }
}
