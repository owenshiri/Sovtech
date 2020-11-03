using System;
using System.Collections.Generic;

namespace ChuckSwapiCAssessment.Domain.Model
{
    public class SearchResult
    {
        public List<Joke> Jokes { get; set; }
        public List<People> People { get; set; }
    }
}
