using System.Threading.Tasks;
using Jokes.Models;

namespace Jokes.Data
{
    public interface IJokesRepository
    {
        Task<Joke> GetRandomJoke();
        Task<SearchJoke> SerachJokes(string searchTerm);
    }
}