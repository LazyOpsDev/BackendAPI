using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LoginModel
    {
        //[BindProperty]
        public string Username { get; set; }
        //[BindProperty]
        public string Password { get; set; }
    }
}
