using StreamBoss.ApiService.Models;
using System.Collections.Generic;

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
            var client = _httpClientFactory.CreateClient("streamboss.ShowApiService");
            var empty = Array.Empty<Show>();
            return await Task.FromResult(empty);
        }
    }
}
