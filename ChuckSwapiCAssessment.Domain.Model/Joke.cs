using System;

namespace ChuckSwapiCAssessment.Domain.Model
{
    public class Joke
    {
        public string[] Categories { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IconUrl { get; set; }
        public string Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }
}
