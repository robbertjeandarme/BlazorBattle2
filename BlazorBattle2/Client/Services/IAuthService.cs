using System.Threading.Tasks;
using BlazorBattle2.Shared;

namespace BlazorBattle2.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);

        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}