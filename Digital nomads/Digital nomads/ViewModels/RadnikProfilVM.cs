using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class RadnikProfilVM
    {
        public string NameAndSurname { get; set; }
        public string JobDescription { get; set; }
        public int NumberOfProjects { get; set; }
        public int NumberOfTrophies { get; set; }
        public DateTime DateOfLastProject { get; set; }
        public int Id { get; set; }
    }
}
