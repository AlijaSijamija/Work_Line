using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class ProjektPrikaziVM
    {
        public int Id { get; set; }
        public string NazivProjekta { get; set; }
        public string DatumPocetka { get; set; }
        public string DatumZavrsetka { get; set; }
        public string Rok { get; set; }
        public string Opis { get; set; }
    }
}
