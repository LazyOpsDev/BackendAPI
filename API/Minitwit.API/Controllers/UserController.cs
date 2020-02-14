using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Minitwit.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("follow")]
        public IActionResult Follow() {
            return Created("TODO", "TODO");
        }

        [HttpPost]
        [Route("unfollow")]
        public IActionResult Unfollow() {
            return Ok("");
        }
    }
}
