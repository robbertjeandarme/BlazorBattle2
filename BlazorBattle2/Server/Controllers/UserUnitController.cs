using System.Linq;
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
    public class UserUnitController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext context , IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _context.Units.FirstOrDefaultAsync<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();

            if (user.Bananas < unit.BananaCost)
            {
                return BadRequest("Not enough bananas !");
            }

            user.Bananas -= unit.BananaCost;

            var newUserUnit = new UserUnit
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.HitPoints
            };

            _context.UserUnits.Add(newUserUnit);
            await _context.SaveChangesAsync();

            return Ok(newUserUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserUnits()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits.Where(unit => unit.UserId == user.Id).ToListAsync();

            var response = userUnits.Select(
                unit => new UserUnitResponse
                {
                    UnitId = unit.UnitId,
                    Hitpoints = unit.HitPoints
                });

            return Ok(response);
        }

        [HttpPost("revive")]
        public async Task<IActionResult> ReviveArmy()
        {
            var user = await _utilityService.GetUser();
            var userUnits = await _context.UserUnits
                .Where(unit => unit.UserId == user.Id)
                .Include(unit => unit.Unit)
                .ToListAsync();

            int bananaReviveCost = 1000;

            if (user.Bananas < bananaReviveCost)
            {
                return BadRequest("Not enough bananas ! You need 1000 to revive ur army");
            }
            
            bool isArmyAlive = true;
            foreach (var unit in userUnits.Where(unit => unit.HitPoints < 100))
            {
                isArmyAlive = false;
                unit.HitPoints = 100;
            }

            if (isArmyAlive)
                return Ok("Your army is at full health !");
            
            user.Bananas -= bananaReviveCost;

            await _context.SaveChangesAsync();

            return Ok("Army revived!");

        }
        
    }
}