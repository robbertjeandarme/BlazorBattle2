using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Server.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string email, string password);

        Task<bool> UserExists(string email);
    }
    
}