using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.Models
{
    public class ProjektniTim
    {
        public int Id { get; set; }
        public Projekt Projekt { get; set; }
        public int ProjektId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int KorisnikID { get; set; }
        public RoleNaProjektu RolaNaProjektu { get; set; }
        public int RolaNaProjektuId { get; set; }

    }
}
