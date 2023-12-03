﻿using StreamBoss.Shared.Models;

namespace StreamBoss.ApiService.Services
{
    public interface IShowDataService
    {
        Task<Show[]> SearchShows(string searchText);
    }
}