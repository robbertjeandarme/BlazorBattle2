using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBattle2.Client.Services
{
    public class BananaService : IBananaService
    {
        private readonly HttpClient _httpClient;
        public event Action OnChange;

        public int Bananas { get; set; } = 0;
        
        public BananaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public void EatBananas(int amount)
        {
            Bananas -= amount;
            BananasChanged();
        }

        void BananasChanged() => OnChange.Invoke();
        
        public async Task AddBananas (int amount)
        {
            var result = await _httpClient.PutAsJsonAsync<int>("/api/user/addbananas" , amount);
            Bananas = await result.Content.ReadFromJsonAsync<int>();
            BananasChanged();   
        }
        
        public async Task GetBananas()
        {
            Bananas = await _httpClient.GetFromJsonAsync<int>("/api/user/getbananas");
            BananasChanged();
        }
        
    }
}