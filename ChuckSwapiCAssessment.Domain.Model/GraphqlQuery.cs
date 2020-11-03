using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ChuckSwapiCAssessment.Domain.Model
{
    public class GraphqlQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public Dictionary<string,object> Variables { get; set; }
    }
}
