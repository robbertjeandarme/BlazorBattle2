using System.Threading.Tasks;
using BlazorBattle2.Server.Data;
using BlazorBattle2.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBattle2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            var response = await _authRepository.Register(
                new User
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    Bananas = request.Bananas,
                    DateOfBirth = request.DateOfBirth,
                    IsConfirmed = request.IsConfirmed
                }, request.Password
            );

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            var response = await _authRepository.Login(request.Email, request.Password);

            if (!response.Succes)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
        
}
}