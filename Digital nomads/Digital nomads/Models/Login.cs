using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Digital_nomads.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string  UserName { get; set; }
        public string Password { get; set; }

    }
}
