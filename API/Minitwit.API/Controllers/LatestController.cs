using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Minitwit.API.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Minitwit.API.Controllers
{
    [LatestFilter]
    public class LatestController : Controller
    {
        private readonly ILastNumberRepository _lastNumberRepository;

        public LatestController(ILastNumberRepository lastNumberRepository)
        {
            _lastNumberRepository = lastNumberRepository;
        }

        [HttpGet("/latest")]
        public async Task<IActionResult> Get()
        {

            return new OkObjectResult(new { latest=  _lastNumberRepository.ReadLatest() });
        }
    }
}
