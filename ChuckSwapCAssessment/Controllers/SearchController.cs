using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuckSwapiCAssessment.Domain.Model;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchApplicationService;

namespace ChuckSwapCAssessment.Controllers
{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {

//        private readonly ISchema schema;
//        private readonly IDocumentExecuter executer;
//        private readonly ISearchService searchService;
//        public SearchController(ISchema schema, IDocumentExecuter executer, ISearchService searchService)
//        {
//            this.schema = schema;
//            this.executer = executer;
//            this.searchService = searchService;
//        }
//        [HttpGet]
//        [Route("{term}")]
//        public async Task<IActionResult> Search(string term)
//        {
//            var searchResult = await searchService.Search(term);
//            return Ok(searchResult);
//        }
//    }
}
