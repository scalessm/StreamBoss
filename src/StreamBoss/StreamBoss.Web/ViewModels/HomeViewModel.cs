using Grpc.Core;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace StreamBoss.Web.ViewModels
{
    public class HomeViewModel
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public HomeViewModel(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;  
        }

        public async Task SetAuthenticationState()
        {
            var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
            SetAuthenticationState(state);
        }

        private async void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var state = await task;
            SetAuthenticationState(state);
        }

        private void SetAuthenticationState(AuthenticationState state)
        {
            if (state.User.Identity.IsAuthenticated)
            {
                User = state.User;
                IsAuthenticated = true;
            }
            else
            {
                User = default;
                IsAuthenticated = false;
            }
        }

        public ClaimsPrincipal User { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
