using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public JokesController(IJokesRepository client)
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
        /// <returns>3 Lists of Jokes Grouped by size</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var searchJoke = await _client.SerachJokes(searchTerm);
            var jokes = FormatJokes(searchJoke);
            GroupedJokes groupedJokes = new GroupedJokes();
            groupedJokes.SmallJokes = jokes.Where(x => x.group_type == Joke.GroupType.small).OrderBy(x=>x.joke.Length).ToList();
            groupedJokes.MediumJokes = jokes.Where(x => x.group_type == Joke.GroupType.medium).OrderBy(x=>x.joke.Length).ToList();
            groupedJokes.LargeJokes = jokes.Where(x => x.group_type == Joke.GroupType.large).OrderBy(x=>x.joke.Length).ToList();

            if (jokes != null)
            {
                return Ok(groupedJokes);
            }
            return NotFound();
        }

        private List<Joke> FormatJokes(SearchJoke searchJoke)
        {
            var jokes = new List<Joke>();
            foreach (var joke in searchJoke.results)
            {
                joke.joke = EmphasizeTerm(joke.joke, searchJoke.search_term);
                joke.group_type = SetGroup(joke.joke);
                jokes.Add(joke);
            }
            return jokes;
        }

        private Joke.GroupType SetGroup(string joke)
        {
            char[] separators = new [] { ' ', '\n', '\r', 't'};
            int wordCount = joke.Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;
            Joke.GroupType grouping = Joke.GroupType.invalid;
            switch (wordCount)
            {
                case var x when x >= 20:
                    grouping = Joke.GroupType.large;
                    break;
                case var x when x < 20 && x >= 10:
                    grouping = Joke.GroupType.medium;
                    break;
                case var x when x < 10:
                    grouping =Joke.GroupType.small;
                    break;
                default:
                    break;
            }
            return grouping;
        }

       
        private string EmphasizeTerm(string joke, string search_term)
        {

            // if (joke.ToUpper().Contains(search_term.ToUpper()))
            // {
            //     joke = joke.Replace(search_term, search_term.ToUpper(), true, null);
            // }

            string pattern = "\\b" + search_term + "\\b";
            string replacement = search_term.ToUpper();
            joke = Regex.Replace(joke, pattern, replacement, RegexOptions.IgnoreCase);

            return joke;
        }
    }

}