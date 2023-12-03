using StreamBoss.Shared.Models;

namespace StreamBoss.Web.Services
{
    public interface IApiClient
    {
        Task<Show[]> SearchShows(string searchText);
    }
}