using System.Collections;
using System.Collections.Generic;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Client.Services
{
    public interface IUnitService
    {
        IList<Unit> Units { get;}
        
        IList<UserUnit> MyUnits { get; set; }

        void AddUnits(int unitId);
    }
}