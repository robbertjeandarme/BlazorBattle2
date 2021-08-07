using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorBattle2.Shared;
using Blazored.Toast.Services;

namespace BlazorBattle2.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _httpClient;
        private readonly IBananaService _bananaService;

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();


        public UnitService(IToastService toastService , HttpClient httpClient , IBananaService bananaService)
        {
            _toastService = toastService;
            _httpClient = httpClient;
            _bananaService = bananaService;
        }

        public async Task AddUnits(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            var result = await _httpClient.PostAsJsonAsync<int>("api/userunit", unitId);

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }
            else
            {
                await _bananaService.GetBananas();
                _toastService.ShowSuccess($"Your{unit.Title} has been build" , "Unit Build ! ");
            }
        }

        public async Task LoadUnitAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _httpClient.GetFromJsonAsync<IList<Unit>>("api/unit");
            }
        }

        public async Task LoadUserUnitAsync()
        {
            MyUnits = await _httpClient.GetFromJsonAsync<IList<UserUnit>>("api/userunit");
        }

        public async Task ReviveArmy()
        {
            var result = await _httpClient.PostAsJsonAsync<string>("api/userunit/revive" , null);

            if (result.StatusCode == System.Net.HttpStatusCode.OK )
            {
                _toastService.ShowSuccess(await result.Content.ReadAsStringAsync());
            }
            else
            {
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            }

            await LoadUserUnitAsync();
            await _bananaService.GetBananas();

        }
    }
}