using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvp_studio_api.Models;
using testApi;

namespace mvp_studio_api.Controllers
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

        // GET: api/TeamMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeam_Member()
        {
          if (_context.Team_Member == null)
          {
              return NotFound();
          }
            return await _context.Team_Member.ToListAsync();
        }

        // GET: api/TeamMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMember>> GetTeamMember(int id)
        {
          if (_context.Team_Member == null)
          {
              return NotFound();
          }
            var teamMember = await _context.Team_Member.FindAsync(id);

            if (teamMember == null)
            {
                return NotFound();
            }

            return teamMember;
        }

        // PUT: api/TeamMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamMember).State = EntityState.Modified;

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

        // POST: api/TeamMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
        {
          if (_context.Team_Member == null)
          {
              return Problem("Entity set 'AppDbContext.Team_Member'  is null.");
          }
            _context.Team_Member.Add(teamMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamMember", new { id = teamMember.Id }, teamMember);
        }

        // DELETE: api/TeamMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            if (_context.Team_Member == null)
            {
                return NotFound();
            }
            var teamMember = await _context.Team_Member.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }

            _context.Team_Member.Remove(teamMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamMemberExists(int id)
        {
            return (_context.Team_Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
