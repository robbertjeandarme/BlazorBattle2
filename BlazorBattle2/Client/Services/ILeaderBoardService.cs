using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Client.Services
{
    public interface ILeaderBoardService
    {
        public IList<UserStatisticResponse> LeaderBoard { get; set; }
        
        Task GetLeaderBoards();
    }
}