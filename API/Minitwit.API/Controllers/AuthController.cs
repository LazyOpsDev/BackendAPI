using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minitwit.API.Util;
using System;
using System.Threading.Tasks;

namespace Minitwit.API.Controllers
{
    [Route("/")]
    [LatestFilter]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepository, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Login() => Ok("You GET Test");

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Unauthorized login attempt via LoginModel: @{model}", model);
                return Unauthorized();
            }

            var res = _userRepository.Login(model);
            if (!res.Equals(Guid.Empty))
            {
                _logger.LogInformation($"User {model.Username} logged in succesfully");
                var options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddSeconds(60);
                HttpContext.Response.Cookies.Append("user", model.Username, options);
                HttpContext.Response.Cookies.Append("userId", res.ToString(), options);
                return RedirectToAction("Root", "Timeline");
            }
            _logger.LogWarning($"User {model.Username} logged in unsuccesfully. Pw: {model.Password}"); // Totally GDPR safe
            return NoContent();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Unauthorized register attempt via RegisterModel: @{model}", model);
                return Unauthorized();
            }

            try
            {
                var res = _userRepository.RegisterUser(model);
                _logger.LogInformation($"User: {model.username} has registered with email: {model.email}");
                var options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddSeconds(60);
                HttpContext.Response.Cookies.Append("user", model.username, options);
                HttpContext.Response.Cookies.Append("userId", res.ToString(), options);
                //return RedirectToAction("Root", "Timeline");

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Some error occured.");
            }

            return NoContent();
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("user");
            HttpContext.Response.Cookies.Delete("userId");
            _logger.LogInformation("User logged out...");
            return Ok("Logged Out");
        }
    }
}
