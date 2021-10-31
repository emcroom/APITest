using System.Collections.Generic;

namespace Jokes.Models
{
    public class SearchJoke
    {
         /// <summary>
        /// Not a preferred code style for lower case properties but it will map well with the outside api
        /// </summary>
        public int current_page { get; set; }
        public int limit { get; set; }
        public int next_page { get; set; }
        public IEnumerable<Joke> results { get; set; }
        public string search_term { get; set; }
        public int status { get; set; }
        public int total_jokes { get; set; }
        public int total_pages { get; set; }
    }
}