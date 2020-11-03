using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuckApplicationService;
using ChuckSwapiCAssessment.Domain.Model;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChuckSwapCAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        private readonly IChuckNorrisService chuckNorrisService;
        public ChuckController(IChuckNorrisService chuckNorrisService)
        {
            this.chuckNorrisService = chuckNorrisService;
        }
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await chuckNorrisService.GetAllCategories();
            return Ok(categories);
        }

    }
}
