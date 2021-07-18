using System.Collections.Generic;
using System.Linq;
using BlazorBattle2.Shared;
using Blazored.Toast.Services;

namespace BlazorBattle2.Client.Services
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        public UnitService(IToastService toastService)
        {
            _toastService = toastService;
        }

        public IList<Unit> Units => new List<Unit>
        {
            new Unit {Id = 1, Title = "Knight", Attack = 10, Defense = 10, BananaCost = 100 , HitPoints = 100},
            new Unit {Id = 2, Title = "Archer" , Attack = 15, Defense = 5, BananaCost = 150, HitPoints = 100},
            new Unit {Id = 3, Title = "Mage" ,Attack = 20, Defense = 1, BananaCost = 200, HitPoints = 100}
        };

        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>();

        public void AddUnits(int unitId)
        {
            var unit = Units.First(unit => unit.Id == unitId);
            MyUnits.Add(new UserUnit {UnitId = unit.Id , HitPoints = unit.HitPoints});
            _toastService.ShowSuccess($"Your{unit.Title} has been build" , "Unit Build ! ");
        }
    }
}