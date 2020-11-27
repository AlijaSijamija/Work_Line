using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class RadnikDashboardVM
    {
        public int RadnikId { get; set; }
        public int ProjektId { get; set; }
        public string ImeIPrezime { get; set; }
        public int ProjektRolaId { get; set; }
        public string ProjektRola { get; set; }
        public int BrojTrofeja { get; set; }
        public int BestScore { get; set; }
        public IEnumerable<TopFive> Najbolji { get; set; }
        public class TopFive
        {
            public int Id { get; set; }
            public int Bodovi { get; set; }
            public int ImeIPrezime { get; set; }
        } 
    }
}
