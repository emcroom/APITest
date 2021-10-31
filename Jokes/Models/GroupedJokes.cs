using System.Collections.Generic;

namespace Jokes.Models
{
    public class GroupedJokes
    {
        /// <summary>
        /// Since the requirements defined a grouped response but not an implementation I decided to create a class
        /// that will return the joke objects in specific groups
        /// </summary>
        public IEnumerable<Joke> SmallJokes { get; set; }
        public IEnumerable<Joke> MediumJokes { get; set; }
        public IEnumerable<Joke> LargeJokes { get; set; }
    }
}