using StreamBoss.ShowApiService.Models.TvMaze;
using StreamBoss.Shared.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamBoss.ShowApiService.Services.TvMaze
{
    public class TvMazeDataService : ITvDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TvMazeDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ShowSearchResult[]> SearchForShow(string query)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                client.BaseAddress = new Uri(TvMazeRoutes.BaseUrl);
                var url = string.Format(TvMazeRoutes.SearchShow, query);
                var results = await client.GetAsync(url);

                if(!results.IsSuccessStatusCode)
                {
                    var error = new RequestError
                    {
                        StatusCode = (int)results.StatusCode,
                        StatusDescription = results.ReasonPhrase,
                        Message = await results.Content.ReadAsStringAsync()
                    };
                }
                    
                var content = await results.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                };
                var result = JsonSerializer.Deserialize<ShowSearchResult[]>(content, options);

                return result;
            }
        }
    }
}
