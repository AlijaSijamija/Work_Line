using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digital_nomads.ViewModels
{
    public class ProjektUcesniciVM
    {

        public int ProjektId { get; set; }
        public IEnumerable<row> rows { get; set; }
        public class row
        {
            public int Id { get; set; }
            public string ImeIPrezime { get; set; }
            public IEnumerable<string> vjestine { get; set; }
            public IEnumerable<SelectListItem> Role { get; set; }
            public int RolaId { get; set; }
        }

      
    }
}
