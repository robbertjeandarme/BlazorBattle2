using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattle2.Server.Data;
using BlazorBattle2.Server.Services;
using BlazorBattle2.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserController(DataContext context  , IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }
        
        [HttpGet("getbananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await _utilityService.GetUser();
            return Ok(user.Bananas);
        }

        [HttpGet("leaderboards")]
        public async Task<IActionResult> GetLeaderBoard()
        {
            var users = await _context.Users.Where(user => !user.IsDeleted && user.IsConfirmed).ToListAsync();

            users = users
                .OrderByDescending(u => u.Victories)
                .ThenBy(u => u.Battles)
                .ThenBy(u => u.Defeats)
                .ThenBy(u => u.DateCreated)
                .ToList();

            int rank = 1;
            var response = users.Select(user => new UserStatisticResponse()
            {
                Rank = rank++,
                UserId = user.Id,
                UserName = user.UserName,
                Victories = user.Victories,
                Battles = user.Battles,
                Defeats = user.Defeats,
            });

            return Ok(response);
        }
        
        [HttpPut("addbananas")]
        public async Task<IActionResult> AddBananas([FromBody] int amount)
        {
            var user = await _utilityService.GetUser();
            user.Bananas += amount;

            await _context.SaveChangesAsync();

            return Ok(user.Bananas);
        }

        
    }
}