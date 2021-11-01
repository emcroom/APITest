using System;
using Xunit;
using Jokes.Data;

namespace JokeTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestRandomJoke()
        {
            var client = new JokesClient();
            var Joke = client.GetRandomJoke().Result;
            Assert.NotNull(Joke);
            Assert.Equal(200, Joke.status);
        }

        [Fact]
        public void TestSearch()
        {
            var searchTerm = "dad";
            var client = new JokesClient();
            var Joke = client.SerachJokes(searchTerm).Result;
            Assert.NotNull(Joke);
            //Check for valid results
            foreach (var joke in Joke.results)
            {
                Assert.Contains(searchTerm, joke.joke.ToLower());
            }

        }
    }

}
