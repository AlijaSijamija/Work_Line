using Digital_nomads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class PersonalStatsTopUsersVM
    {
        public List<Tuple<Korisnik, int>> tuples { get; set; }
        public PersonalStatisticUserVM personalStats { get; set; }
        public string name { get; set; }
    }
}
