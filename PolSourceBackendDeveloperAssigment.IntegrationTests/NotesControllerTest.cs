using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PolSourceBackendDeveloperAssignment;
using PolSourceBackendDeveloperAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PolSourceBackendDeveloperAssigment.IntegrationTests
{
    public class NotesControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public NotesControllerTest(CustomWebApplicationFactory<Startup> factory)
        {

            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        //GetAll

        [Fact]
        public async Task GetNotes_AllNotesCountEqual_OK()
        {
            var response = await _client.GetAsync("/api/Notes");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            Assert.True(result.Count()>0);
        }

        //GetId

        [Fact]
        public async Task GetNote_NoteDoesNotExist_NotFound()
        {
            var id = 999;
            var response = await _client.GetAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetNote_NoteDoesExist_OK()
        {
            var id = 3;
            var response = await _client.GetAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Note>(stringResponse);
            Assert.Equal("To jest trzecia notatka", result.Title);
            Assert.Equal("To jest zawartosc trzeciej notatki", result.Content);
        }

        //Delete

        [Fact]
        public async Task DeleteNote_NoteDeletedCount_OK()
        {
            var id = 17;

            var response = await _client.GetAsync("/api/Notes");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            var count = res.Count();

            response = await _client.DeleteAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Note>(stringResponse);
            Assert.Equal(17, result.IdNote);

            response = await _client.GetAsync("/api/Notes");
            stringResponse = await response.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);

            Assert.Equal(res.Count(), --count);
        }


        [Fact]
        public async Task DeleteNote_CheckIfHistoryExists_OK()
        {
            var id = 18;

            var response = await _client.DeleteAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"api/NoteHistories/{id}");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<NoteHistory>>(stringResponse);

            Assert.True(result.Count()>0);
        }

        [Fact]
        public async Task DeleteNote_DeletedNoteNotExist_OK()
        {
            var id = 4;
            var response = await _client.DeleteAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _client.GetAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteNote_NoteDoesNotExist_NotFound()
        {
            var id = 999;

            var response = await _client.DeleteAsync($"/api/Notes/{id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //Post
        //TODO: Sprawdzic z³y input w post

        [Fact]
        public async Task PostNote_AddNewNote_BadRequest()
        {
            var response = await _client.PostAsync("api/Notes", new StringContent(
                JsonConvert.SerializeObject(new Note()
                {
                }), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostNote_AddNewNoteCount_OK()
        {
            var response = await _client.GetAsync("api/Notes");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            var count = res.Count();

            response = await _client.PostAsync("api/Notes", new StringContent(
                JsonConvert.SerializeObject(new Note(){
                    Title = "To jest nowy tytul",
                    Content = "A to jest zawartosc tej notatki"
                }), Encoding.UTF8, "application/json"));

            response = await _client.GetAsync("api/Notes");
            stringResponse = await response.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            Assert.Equal(++count, res.Count());
        }

        [Fact]
        public async Task PostNote_AddCheckHistory_OK()
        {
            var response = await _client.GetAsync("api/NoteHistories/");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            var count = res.Count();

            response = await _client.PostAsync("api/Notes", new StringContent(
                JsonConvert.SerializeObject(new Note()
                {
                    Title = "To jest nowy tytul",
                    Content = "A to jest zawartosc tej notatki"
                }), Encoding.UTF8, "application/json"));

            response = await _client.GetAsync("api/NoteHistories/");
            stringResponse = await response.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            Assert.Equal(++count, res.Count());
        }

        //Put

        [Fact]
        public async Task PutNote_NoteNotExist_NotFound()
        {
            var id = 999;

            var response = await _client.PutAsync($"/api/Notes/{id}", new StringContent(
                JsonConvert.SerializeObject(new Note()
                {
                    IdNote = id,
                    Title = "To jest nowy tytul",
                    Content = "A to jest zawartosc tej notatki"
                }), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutNote_NoteIdNotEqual_BadRequest()
        {
            var id1 = 999;
            var id2 = 888;

            var response = await _client.PutAsync($"/api/Notes/{id1}", new StringContent(
                JsonConvert.SerializeObject(new Note()
                {
                    IdNote =  id2,
                    Title = "To jest nowy tytul",
                    Content = "A to jest zawartosc tej notatki"
                }), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task PutNote_UpdateNote_OK()
        {
            var id = 9;

            var response = await _client.PutAsync($"/api/Notes/{id}", new StringContent(
               JsonConvert.SerializeObject(new Note()
               {
                   IdNote = id,
                   Title = "Tu zmieniam Tytul",
                   Content = "A tu zmieniam zawartosc notatki",
                   Created = DateTime.Today
               }), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        }

        [Fact]
        public async Task PutNote_AddCheckHistory_OK()
        {
            var id = 9;

            var response = await _client.GetAsync("api/NoteHistories/");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            var count = res.Count();

            

            response = await _client.PutAsync($"/api/Notes/{id}", new StringContent(
               JsonConvert.SerializeObject(new Note()
               {
                   IdNote = id,
                   Title = "Tu zmieniam Tytul",
                   Content = "A tu zmieniam zawartosc notatki",
                   Created = DateTime.Today
               }), Encoding.UTF8, "application/json"));

            response = await _client.GetAsync("api/NoteHistories/");
            stringResponse = await response.Content.ReadAsStringAsync();
            res = JsonConvert.DeserializeObject<IEnumerable<Note>>(stringResponse);
            Assert.Equal(++count, res.Count());
        }

    }
}
