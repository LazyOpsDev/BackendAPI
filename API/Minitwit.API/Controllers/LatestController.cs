using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Minitwit.API.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Minitwit.API.Controllers
{
    [LatestFilter]
    public class LatestController : Controller
    {
        private readonly ILastNumberRepository _lastNumberRepository;
        private readonly ILogger<LatestController> _logger;

        public LatestController(ILastNumberRepository lastNumberRepository, ILogger<LatestController> logger)
        {
            _lastNumberRepository = lastNumberRepository;
            _logger = logger;
        }

        [HttpGet("/latest")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetched latest endpoint");
            return new OkObjectResult(new { latest=  _lastNumberRepository.ReadLatest() });
        }
    }
    //public class latestModel
    //{
    //    public int latest { get; set; }
    //}
}
