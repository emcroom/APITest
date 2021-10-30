using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jokes.Data;
using Jokes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Jokes.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JokesController : ControllerBase
    {
        private IJokesRepository _client;
        private readonly ILogger<JokesController> _logger;
        public JokesController(IJokesRepository client, ILogger<JokesController> logger)
        {
            _client = client;

        }
        /// <summary>
        /// Get a Random Joke From icanhazdadjoke.com 
        /// </summary>
        /// <returns>A Joke: string id, string joke, int? status</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Random()
        {
            var joke = await _client.GetRandomJoke();
            if (joke != null)
            {
                return Ok(joke);
            }
            return NotFound();
        }
        /// <summary>
        /// Searches jokes on icanhazdadjoke.com; Limit 30
        /// </summary>
        /// <param name="searchTerm">String for a search term</param>
        /// <returns>List of Joke</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var searchJoke = await _client.SerachJokes(searchTerm);
            var jokes = EmphasizeTerm(searchJoke);
            if (jokes != null)
            {
                return Ok(jokes);
            }
            return NotFound();
        }

        /// <summary>
        /// Upper case the search term in the result set
        /// </summary>
        /// <param name="searchJoke">Response from the Jokes Client</param>
        /// <returns>Emphasized List of Joke</returns>
        private List<Joke> EmphasizeTerm(SearchJoke searchJoke)
        {
            var jokes = new List<Joke>();
            foreach (var joke in searchJoke.results)
            {
                if (joke.joke.ToUpper().Contains(searchJoke.search_term.ToUpper()))
                {
                    joke.joke = joke.joke.Replace(searchJoke.search_term, searchJoke.search_term.ToUpper(), true, null);
                }
                jokes.Add(joke);
            }
            return jokes;
        }
    }

}