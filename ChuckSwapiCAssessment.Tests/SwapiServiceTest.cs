using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChuckSwapCAssessment.API;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System;

namespace ChuckSwapiCAssessment.Tests
{
    [TestClass]
    public class SwapiServiceTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;
        public SwapiServiceTest()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            server = new TestServer(new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                );
            client = server.CreateClient();
        }
        [TestMethod]
        public async Task Test_GetAllPeople()
        {
            var response = await client.GetAsync($"/api/Swapi/people/");
            var people = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<People>>(people, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Assert.IsInstanceOfType(result, typeof(List<People>));
            Assert.IsTrue(result.Count<=10);
            Assert.IsNotNull(result);
        }
    }
}
