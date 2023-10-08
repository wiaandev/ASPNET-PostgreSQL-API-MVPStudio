using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvp_studio_api.Models;
using mvp_studio_api.Models.DTO;
using testApi;

namespace mvp_studio_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAssignedsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectAssignedsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectAssigneds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectAssigned>>> GetProjectAssigned()
        {
            if (_context.ProjectAssigned == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var assignedProject = await (from assigned in _context.ProjectAssigned
                                         join employees in _context.Employee
                                         on assigned.EmployeeId equals employees.Id
                                         select new ProjectAssignedDTO()
                                         {
                                             Id = assigned.Id,
                                             Name = employees.Name,
                                             Surname = employees.Surname,
                                             CurrHours = employees.Curr_Hours,
                                             ProfileImg = employees.ProfileImg,
                                         }).ToListAsync();

            return Ok(assignedProject);
        }

        // GET: api/ProjectAssigneds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectAssigned>> GetProjectAssigned(int id)
        {
          if (_context.ProjectAssigned == null)
          {
              return NotFound();
          }
            var projectAssigned = await _context.ProjectAssigned.FindAsync(id);

            if (projectAssigned == null)
            {
                return NotFound();
            }

            return projectAssigned;
        }

        // PUT: api/ProjectAssigneds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectAssigned(int id, ProjectAssigned projectAssigned)
        {
            if (id != projectAssigned.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectAssigned).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectAssignedExists(id))
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

        // POST: api/ProjectAssigneds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectAssigned>> PostProjectAssigned(ProjectAssigned projectAssigned)
        {
          if (_context.ProjectAssigned == null)
          {
              return Problem("Entity set 'AppDbContext.ProjectAssigned'  is null.");
          }
            _context.ProjectAssigned.Add(projectAssigned);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectAssigned", new { id = projectAssigned.Id }, projectAssigned);
        }

        // DELETE: api/ProjectAssigneds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAssigned(int id)
        {
            if (_context.ProjectAssigned == null)
            {
                return NotFound();
            }
            var projectAssigned = await _context.ProjectAssigned.FindAsync(id);
            if (projectAssigned == null)
            {
                return NotFound();
            }

            _context.ProjectAssigned.Remove(projectAssigned);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectAssignedExists(int id)
        {
            return (_context.ProjectAssigned?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
