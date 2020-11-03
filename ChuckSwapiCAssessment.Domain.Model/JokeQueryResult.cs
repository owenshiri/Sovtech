using System.Collections.Generic;

namespace ChuckSwapiCAssessment.Domain.Model
{
    public class JokeQueryResult
    {
        public int Total { get; set; }
        public List<Joke> Result { get; set; }
    }
}
