using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                                orderby projs.Project_Start ascending
                                 select new ProjectDTO()
                                {
                                    Id = projs.Id,
                                    ClienName = clients.Name,
                                    Project_Name = projs.Project_Name,
                                    Description = projs.Description,
                                    Project_Start = projs.Project_Start,
                                    Duration_Week = projs.Duration_Week,
                                    Project_Time = projs.Project_Time,
                                    Project_Type = projs.Project_Type,
                                    Project_Cost = projs.Project_Cost,
                                    Amount_Paid = projs.Amount_Paid,
                                    isCompleted = projs.isCompleted,
                                    Progress = projs.Progress,
                                  }).ToListAsync();
           Console.WriteLine(projects);
           return Ok(projects);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
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

            var client = await _context.Client.FindAsync(project.ClientId);

            if(client == null)
            {
                return NotFound("Client not found");
            }

            var singleReturnProject = new ProjectDTO()
            {
                Id = project.Id,
                ClienName = client.Name,
                Project_Name = project.Project_Name,
                Description = project.Description,
                Project_Start = project.Project_Start,
                Duration_Week = project.Duration_Week,
                Project_Time = project.Project_Time,
                Project_Type = project.Project_Type,
                Project_Cost = project.Project_Cost,
                Amount_Paid = project.Amount_Paid,
                isCompleted = project.isCompleted,
                Progress = project.Progress,
            };

            return singleReturnProject;
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
        public async Task<ActionResult<ProjectDTO>> PostProject([FromBody] ProjectDTO projectCreateDTO)
        {
            if (projectCreateDTO == null)
            {
                return BadRequest("Invalid data provided for project creation.");
            }

            try
            {
                // Look up the ClientId based on the provided ClientName
                var client = await _context.Client.FirstOrDefaultAsync(c => c.Name == projectCreateDTO.ClienName);

                if (client == null)
                {
                    return BadRequest($"Client with name '{projectCreateDTO.ClienName}' not found.");
                }

                var project = new Project
                {
                    ClientId = client.Id,
                    Project_Name = projectCreateDTO.Project_Name,
                    Description = projectCreateDTO.Description,
                    Project_Start = projectCreateDTO.Project_Start,
                    Duration_Week = projectCreateDTO.Duration_Week,
                    Project_Time = projectCreateDTO.Duration_Week * 40,
                    Project_Type = projectCreateDTO.Project_Type,
                    Project_Cost = projectCreateDTO.Project_Cost,
                };

                _context.Project.Add(project);
                await _context.SaveChangesAsync();

                // You can create a ProjectDTO to return the created project if needed

                return CreatedAtAction("GetProject", new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                // Handle the error here, you can log it or return an appropriate error response.
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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
