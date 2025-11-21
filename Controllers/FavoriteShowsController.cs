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
    public class FavoriteShowsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteShowsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteShow>>> GetFavoriteShows([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                var firstFive = await _context.FavoriteShows
                    .OrderBy(s => s.Id)
                    .Take(5)
                    .ToListAsync();

                return Ok(firstFive);
            }

            var results = await _context.FavoriteShows
                .Where(s => s.Id == id.Value)
                .ToListAsync();

            if (results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
        }


        [HttpPost]
        public async Task<ActionResult<FavoriteShow>> PostFavoriteShow(FavoriteShow show)
        {
            _context.FavoriteShows.Add(show);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavoriteShows), new { id = show.Id }, show);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoriteShow(int id, FavoriteShow show)
        {
            if (id != show.Id)
            {
                return BadRequest();
            }

            _context.Entry(show).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteShowExists(id))
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
        public async Task<IActionResult> DeleteFavoriteShow(int id)
        {
            var show = await _context.FavoriteShows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.FavoriteShows.Remove(show);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteShowExists(int id)
        {
            return _context.FavoriteShows.Any(e => e.Id == id);
        }
    }
}
