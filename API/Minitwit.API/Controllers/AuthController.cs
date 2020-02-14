using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minitwit.Models;

namespace Minitwit.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult Login() => Ok("You GET Test");

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password) {
            return Ok("Called /Login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user) {
            return Created("TODO", "TODO");
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout() {
            return Ok("Logged Out");
        }
    }
}
