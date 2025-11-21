using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakfastFoodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BreakfastFoodsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreakfastFood>>> GetBreakfastFoods([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                var firstFive = await _context.BreakfastFoods
                    .OrderBy(b => b.Id)
                    .Take(5)
                    .ToListAsync();

                return Ok(firstFive);
            }

            
            var results = await _context.BreakfastFoods
                .Where(b => b.Id == id.Value)
                .ToListAsync();

            if (results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<BreakfastFood>> PostBreakfastFood(BreakfastFood food)
        {
            _context.BreakfastFoods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBreakfastFoods), new { id = food.Id }, food);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreakfastFood(int id, BreakfastFood food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreakfastFoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreakfastFood(int id)
        {
            var food = await _context.BreakfastFoods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.BreakfastFoods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BreakfastFoodExists(int id)
        {
            return _context.BreakfastFoods.Any(e => e.Id == id);
        }
    }
}