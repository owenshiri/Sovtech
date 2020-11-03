using System.Collections.Generic;

public class PeopleQueryResult
{
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public List<People> Results { get; set; }
}




