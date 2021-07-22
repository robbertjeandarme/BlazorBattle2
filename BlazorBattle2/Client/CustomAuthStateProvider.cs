using System.Reflection.Emit;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorBattle2.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        /// <summary>
        /// we maken een lege user aan (empty betekend dat we niet Autenticated zijn )
        ///
        /// we gaan kijken in de localStorage of de bool autentiacted aanwezig is
        ///
        /// wanner je returnen wij een autenticated user en raisen we NotifyAuthnetiacionStateChagend event
        ///
        /// waneer niet raisen we dat zelde event maar zijn we natuurlijk nier Autenticated
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            var state = new AuthenticationState(new ClaimsPrincipal());
            if (await _localStorageService.GetItemAsync<bool>("isAuthenticated"))
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "Robbert")
                }, "Test authentication type");

                var user = new ClaimsPrincipal(identity);
                state = new AuthenticationState(user);
            }
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;


        }
    }
}