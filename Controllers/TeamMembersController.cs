using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMembersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamMembersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers([FromQuery] int? id)
        {
            if (id == null || id == 0)
            {
                var firstFive = await _context.TeamMembers
                    .OrderBy(t => t.Id)
                    .Take(5)
                    .ToListAsync();

                return Ok(firstFive);
            }

            var results = await _context.TeamMembers
                .Where(t => t.Id == id.Value)
                .ToListAsync();

            if (results.Count == 0)
            {
                return NotFound();
            }

            return Ok(results);
        
        }

       
        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember member)
        {
            _context.TeamMembers.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeamMembers), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember member)
        {
            if (id != member.Id)
            {
                return BadRequest();
            }

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMemberExists(id))
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
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }
    }
}