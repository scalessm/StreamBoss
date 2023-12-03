using StreamBoss.Shared.Models.TvMaze;

namespace StreamBoss.ShowApiService.Services
{
    public interface ITvDataService
    {
        Task<ShowSearchResult[]> SearchForShow(string query);
    }
}
