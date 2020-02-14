using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Minitwit.API.Controllers
{
    [ApiController]
    public class TimelineController : ControllerBase
    {

        // GET: api/Timeline
        [HttpGet]
        public IActionResult Root()
        {
            return Ok("Called /");
        }

        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return Ok("Called /public");
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Private(string username)
        {
            return Ok($"Called /{username}");
        }

        [HttpPost]
        [Route("add_message")]
        public IActionResult AddMessage(string tweet) {
            return Created("TODO", "TODO");
        }


    }
}