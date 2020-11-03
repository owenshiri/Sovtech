using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using ChuckSwapiCAssessment.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChuckSwapCAssessment.API;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;

namespace ChuckSwapiCAssessment.Tests
{
    [TestClass]
    public class ChuckNorrisServiceTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;
        public ChuckNorrisServiceTest()
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
        public async Task Test_GetCategories()
        {
            var response = await client.GetAsync("/api/Chuck/categories");
            var categories = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<string>>(categories, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Assert.IsInstanceOfType(result,typeof(List<string>));
            Assert.IsNotNull(result);
        }
    }
}
