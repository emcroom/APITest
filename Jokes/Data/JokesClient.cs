using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Jokes.Models;
using Microsoft.Extensions.Logging;

namespace Jokes.Data
{

    public class JokesClient : IJokesRepository
    {
        private HttpClient _client;
        private readonly ConfigurationData _config;
        private readonly ILogger<JokesClient> _logger;

        public JokesClient(HttpClient client, IOptions<ConfigurationData> config, ILogger<JokesClient> logger)
        {
            _client = client;
            _config = config.Value;
            _logger = logger;

            _client.BaseAddress = new Uri(_config.BaseURL);
            _client.DefaultRequestHeaders.Add("Accept", _config.Accept);
            _client.DefaultRequestHeaders.Add("User-Agent", _config.UserAgent);

        }

        public async Task<Joke> GetRandomJoke()
        {

            try
            {
                var response = await _client.GetAsync("");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Joke>();

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Random Joke Exception whe connecting with API:  {ex.ToString()}");
                return null;
            }

        }

        public async Task<SearchJoke> SerachJokes(string searchTerm)
        {

            try
            {
                var url = new Uri($"search?limit=30&term=" + searchTerm, UriKind.Relative);
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<SearchJoke>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Search Joke Exception whe connecting with API:  {ex.ToString()}");
                return null;
            }
        }


    }

}