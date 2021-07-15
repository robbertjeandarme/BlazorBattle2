using System;

namespace BlazorBattle2.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;
        
        int Bananas { get; set; }

        public void EatBananas(int amount);

        public void AddBananas(int amount);
    }
}