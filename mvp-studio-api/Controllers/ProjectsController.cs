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
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
        {
          if (_context.Project == null)
          {
                return StatusCode(StatusCodes.Status500InternalServerError);
          }
            //return await _context.Project.ToListAsync();
           var projects = await (from projs in _context.Project
                                join clients in _context.Client
                                on projs.ClientId equals clients.Id
                                  select new ProjectDTO()
                                {
                                    Id = projs.Id,
                                    ClienName = clients.Name,
                                    Project_Name = projs.Project_Name,
                                    Description = projs.Description,
                                    Project_Time = projs.Project_Time,
                                    Project_Type = projs.Project_Type,
                                    Project_Cost = projs.Project_Cost,
                                    Amount_Paid = projs.Amount_Paid
                                 }).ToListAsync();

           Console.WriteLine(projects);
           return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
          if (_context.Project == null)
          {
              return NotFound();
          }
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
          if (_context.Project == null)
          {
              return Problem("Entity set 'AppDbContext.Project'  is null.");
          }
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (_context.Project == null)
            {
                return NotFound();
            }
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return (_context.Project?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
