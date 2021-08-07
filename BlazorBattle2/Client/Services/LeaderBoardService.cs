using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Client.Services
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly HttpClient _httpClient;
        
        public IList<UserStatisticResponse> LeaderBoard { get; set; }

        public LeaderBoardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task GetLeaderBoards()
        {
            LeaderBoard = await _httpClient.GetFromJsonAsync<IList<UserStatisticResponse>>("api/user/leaderboards");
        }
    }
}