using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PolSourceBackendDeveloperAssignment;
using PolSourceBackendDeveloperAssignment.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PolSourceBackendDeveloperAssigment.IntegrationTests
{
    public class NoteHistoriesControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public NoteHistoriesControllerTest(CustomWebApplicationFactory<Startup> factory)
        {

            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        //GetAll

        [Fact]
        public async Task GetNotesHistory_AllNotesCount_OK()
        {
            var response = await _client.GetAsync("api/NoteHistories");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<NoteHistory>>(stringResponse);
            Assert.True(result.Count() > 0);
        }

        //GetId

        [Fact]
        public async Task GetNote_NoteDoesExist_OK()
        {
            var id = 9;
            var response = await _client.GetAsync($"api/NoteHistories/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
