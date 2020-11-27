using Digital_nomads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class TopUsersVM
    {
        public List<Tuple<Korisnik, int>> tuples { get; set; }
        public string name { get; set; }
        public List<string> Projekti { get; set; }

    }
}
