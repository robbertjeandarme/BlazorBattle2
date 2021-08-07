using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Client.Services
{
    public interface IUnitService
    {
        IList<Unit> Units { get; set; }
        
        IList<UserUnit> MyUnits { get; set; }

        Task AddUnits(int unitId);

        Task LoadUnitAsync();

        Task LoadUserUnitAsync();

        Task ReviveArmy();
    }
}