using StreamBoss.ApiService.Models;
using StreamBoss.Shared.Exceptions;
using StreamBoss.Shared.Models;
using System.Collections.Generic;
using tvMaze = StreamBoss.Shared.Models.TvMaze;

namespace StreamBoss.ApiService.Services
{
    public class ShowDataService : IShowDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShowDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Show[]> SearchShows(string searchText)
        {
            using (var client = _httpClientFactory.CreateClient("ShowApi"))
            {
                var response = await client.GetAsync($"/shows?searchTerm={searchText}");
                if (response.IsSuccessStatusCode)
                {
                    var shows = await response.Content.ReadFromJsonAsync<tvMaze.ShowSearchResult[]>();

                    var results = shows != null ? shows.Select(show => new Show
                    {
                        Id = show.Show.Id,
                        Title = show.Show.Name,
                        ImageUrl = show.Show?.Image?.Medium,
                        Summary = show.Show.Summary
                    }).ToArray() : Array.Empty<Show>();

                    return results;
                }
                else
                {
                    var error = new RequestError
                    {
                        StatusCode = (int)response.StatusCode,
                        StatusDescription = response.ReasonPhrase,
                        Message = await response.Content.ReadAsStringAsync()
                    };

                    throw new ApiRequestException(error);
                }
            }
        }
    }
}
