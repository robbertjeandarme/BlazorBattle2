using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Server.Services
{
    public interface IUtilityService
    {
        public Task<User> GetUser();
    }
}