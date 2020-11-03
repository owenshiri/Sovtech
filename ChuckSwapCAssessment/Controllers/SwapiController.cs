using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SwapiApplicationService;

namespace ChuckSwapCAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiService swapiService;
        public SwapiController(ISwapiService swapiService)
        {
            this.swapiService = swapiService;
        }
        [HttpGet]
        [Route("people")]
        public IActionResult GetAll()
        {
            var result = swapiService.GetAllPeoples();
            return Ok(result);
        }
    }
}
