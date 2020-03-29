using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minitwit.API.Util;
using Minitwit.Models;

namespace Minitwit.API.Controllers
{
    [Route("/")]
    [LatestFilter]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Login() => Ok("You GET Test");

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model) {
            if (!ModelState.IsValid)
                return Unauthorized();

            var res = _userRepository.Login(model);
            if(!res.Equals(Guid.Empty))
            {
                var options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddSeconds(60);
                HttpContext.Response.Cookies.Append("user", model.Username, options);
                HttpContext.Response.Cookies.Append("userId", res.ToString(), options);
                return RedirectToAction("Root", "Timeline");
            }

            return NoContent();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model) {
            if (!ModelState.IsValid)
                return Unauthorized();

            try
            {
                var res = _userRepository.RegisterUser(model);
                var options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddSeconds(60);
                HttpContext.Response.Cookies.Append("user", model.username, options);
                HttpContext.Response.Cookies.Append("userId", res.ToString(), options);
                //return RedirectToAction("Root", "Timeline");

            } catch(Exception e)
            {

            }

            return NoContent();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout() {
            HttpContext.Response.Cookies.Delete("user");
            HttpContext.Response.Cookies.Delete("userId");
            return Ok("Logged Out");
        }
    }
}
