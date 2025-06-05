using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using AspNetApp.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace AspNetApp.Controllers
{
    public class PeopleController : ODataController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static List<Person> _people = new List<Person>();

        public PeopleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET method
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            if (!_people.Any())
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetFromJsonAsync<JsonElement>("https://randomuser.me/api/?results=10");

                var people = response
                    .GetProperty("results")
                    .EnumerateArray()
                    .Select((p, index) => new Person
                    {
                        Id = index + 1,
                        FullName = $"{p.GetProperty("name").GetProperty("first").GetString()} {p.GetProperty("name").GetProperty("last").GetString()}",
                        Email = p.GetProperty("email").GetString() ?? "",
                    })
                    .ToList();

                _people.AddRange(people);
            }

            return Ok(_people);
        }

        // POST method
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
                return BadRequest("Client data is null.");

            person.Id = _people.Count + 1;

            _people.Add(person);
            return Created(person);
        }

        // DELETE method
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _people.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound("Person not found.");
            }

            _people.Remove(person);
            return NoContent(); // Successfully deleted
        }
    }
}
