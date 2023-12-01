using StreamBoss.MovieApiService.Models.TvMaze;

namespace StreamBoss.MovieApiService.Services
{
    public interface ITvDataService
    {
        Task<ShowSearchResult[]> SearchForShow(string query);
    }
}
