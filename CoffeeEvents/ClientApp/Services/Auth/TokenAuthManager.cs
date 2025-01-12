using Blazored.LocalStorage;
using Domain;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientApp.Services.Auth;

public class TokenAuthManager
{
    private readonly ILocalStorageService _localStorageService;
    private readonly TokenAuthenticationStateProvider _authenticationStateProvider;

    public TokenAuthManager(ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _localStorageService = localStorageService;
        _authenticationStateProvider = (TokenAuthenticationStateProvider)authenticationStateProvider;
    }

    public async Task SetRefreshToken(string refreshToken)
    {
        await _localStorageService.SetItemAsStringAsync(AuthOptions.RefreshTokenCookieName, refreshToken);
    }

    public async Task<string?> GetRefreshToken()
    {
        if (await _localStorageService.ContainKeyAsync(AuthOptions.RefreshTokenCookieName))
        {
            return await _localStorageService.GetItemAsStringAsync(AuthOptions.RefreshTokenCookieName);
        }
        return null;
    }
    
    public async Task LoginUserAsync(string accessToken)
    {
        await _localStorageService.SetItemAsStringAsync("authToken", accessToken);
        _authenticationStateProvider.NotifyUserAuthentication(accessToken);
    }

    public async Task LogoutUserAsync()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        await _localStorageService.RemoveItemAsync(AuthOptions.RefreshTokenCookieName);
        _authenticationStateProvider.NotifyUserLogout();
    }

    public async Task<AuthenticationState> GetAuthenticationState()
    {
        return await _authenticationStateProvider.GetAuthenticationStateAsync();
    }
    
    public async Task<bool> IsUserLoggedAsync()
    {
        var state = await GetAuthenticationState();
        return state.User.Identity != null && state.User.Identity.IsAuthenticated;
    }

    public async Task<Guid?> TryGetUserId()
    {
        var state = await GetAuthenticationState();
        var claim = state.User.Claims.FirstOrDefault(c => c.Type == AuthOptions.ClaimTypeUserId);
        if (claim == null)
        {
            return null;
        }
        return new Guid(claim.Value);
    }
    
    public async Task<string?> TryGetAccessToken()
    {
        if (await _localStorageService.ContainKeyAsync("authToken"))
        {
            return await _localStorageService.GetItemAsStringAsync("authToken");
        }
        return null;
    }
}