using StreamBoss.Shared.Exceptions;
using StreamBoss.Shared.Models;

namespace StreamBoss.Web.Services
{
    public class ApiClient : IApiClient
    {
        IHttpClientFactory _httpClientFactory;

        public ApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Show[]> SearchShows(string searchText)
        {
            using (var client = _httpClientFactory.CreateClient("Api"))
            {
                var response = await client.GetAsync("shows?searchTerm={searchTerm}");

                if (response.IsSuccessStatusCode)
                {
                    var shows = await response.Content.ReadFromJsonAsync<Show[]>();

                    return shows ?? Array.Empty<Show>();
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
