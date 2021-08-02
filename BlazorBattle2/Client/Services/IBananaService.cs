using System;
using System.Threading.Tasks;

namespace BlazorBattle2.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;
        
        int Bananas { get; set; }

        public void EatBananas(int amount);

        Task AddBananas(int amount);

        Task GetBananas();
    }
}