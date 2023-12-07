using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using StreamBoss.Shared.Models;
using StreamBoss.Web.Services;

namespace StreamBoss.Web.ViewModels
{
    public class ShowSearchViewModel
    {
        private readonly IApiClient _apiClient;

        public ShowSearchViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public Show[] Shows { get; set; } = Array.Empty<Show>();
        public string SearchText { get; set; } = string.Empty;

        public async Task Search()
        {
            Shows = await _apiClient.SearchShows(SearchText);
        }
    }
}
