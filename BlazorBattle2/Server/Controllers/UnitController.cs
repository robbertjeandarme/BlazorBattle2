using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBattle2.Server.Data;
using BlazorBattle2.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly DataContext _context;

        public UnitController( DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var units = await _context.Units.ToListAsync();
            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync()); 
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id, Unit unit)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("De unit had not been found !");
            }

            dbUnit.Title = unit.Title;
            dbUnit.Attack = unit.Attack;
            dbUnit.Defense = unit.Defense;
            dbUnit.HitPoints = unit.HitPoints;
            dbUnit.BananaCost = unit.BananaCost;

            await _context.SaveChangesAsync();
            return Ok(dbUnit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var dbUnit = await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (dbUnit == null)
            {
                return NotFound("De unit had not been found !");
            }

            _context.Units.Remove(dbUnit);
            
            await _context.SaveChangesAsync();
            return Ok(await _context.Units.ToListAsync());
        }
        
}
}