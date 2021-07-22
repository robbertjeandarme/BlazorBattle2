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
        
        public UnitService(IToastService toastService , HttpClient httpClient)
        {
            _toastService = toastService;
            _httpClient = httpClient;
        }

        public IList<Unit> Units { get; set; } = new List<Unit>();

        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnits(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnit {UnitId = unit.Id , HitPoints = unit.HitPoints});
            _toastService.ShowSuccess($"Your{unit.Title} has been build" , "Unit Build ! ");
        }

        public async Task LoadUnitAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _httpClient.GetFromJsonAsync<IList<Unit>>("api/Unit");
            }
        }
    }
}