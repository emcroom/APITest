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
        /// <summary>
        /// set up the private variable in the class from the dependency injection
        /// </summary>
        private readonly HttpClient _client;
        private readonly ConfigurationData _config;
        private readonly ILogger<JokesClient> _logger;

        /// <summary>
        /// Construct the JokesClient
        /// </summary>
        /// <param name="client"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public JokesClient(HttpClient client, IOptions<ConfigurationData> config, ILogger<JokesClient> logger)
        {
            //Set up the private vars
            _client = client;
            _config = config.Value;
            _logger = logger;
            //define client values
            _client.BaseAddress = new Uri(_config.BaseURL);
            _client.DefaultRequestHeaders.Add("Accept", _config.Accept);
            _client.DefaultRequestHeaders.Add("User-Agent", _config.UserAgent);

        }
        /// <summary>
        /// Parameterless constructor for xunit.  Look into xunit DI
        /// </summary>
        public JokesClient()
        {
            //Set up the private vars
            _client = new HttpClient();
            // _logger = logger;
            //define client values
            _client.BaseAddress = new Uri("https://icanhazdadjoke.com/");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("User-Agent", "Croom Take Home Test");

        }
        /// <summary>
        /// Getsy a random joke from the outside api
        /// </summary>
        /// <returns>joke</returns>
        public async Task<Joke> GetRandomJoke()
        {

            try
            {
                //base url provides random response
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
        /// <summary>
        /// Search the outside api for data
        /// </summary>
        /// <param name="searchTerm">String to be searched</param>
        /// <returns>Jokes with meta data</returns>
        public async Task<SearchJoke> SerachJokes(string searchTerm)
        {

            try
            {
                var url = new Uri($"search?limit=30&term={searchTerm}", UriKind.Relative);
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