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
                                join teams in _context.Team on projs.TeamAssigned equals teams.Id into teamJoin
                                from team in teamJoin.DefaultIfEmpty()
                                 orderby projs.Project_Start ascending
                                 select new ProjectDTO()
                                {
                                    Id = projs.Id,
                                    ClienName = clients.Name,
                                    ClientProfileImg = clients.ImgUrl,
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
                                    TeamAssigned = team.TeamName
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

            var team = await _context.Team.FindAsync(project.TeamAssigned);

            if(team == null)
            {
                return NotFound("Team cannot be found");
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
                TeamAssigned = team.TeamName
            };

            return singleReturnProject;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDTO projectUpdateDTO)
        //{
        //    if (id != projectUpdateDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(projectUpdateDTO).State = EntityState.Modified;

        //    var project = await _context.Project.FindAsync(id);
        //    var team = await _context.Team.FirstOrDefaultAsync(t => t.TeamName == projectUpdateDTO.TeamAssigned);

        //    if (project == null)
        //    {
        //        return NotFound($"Project with ID {id} not found.");
        //    }

        //    try
        //    {
        //        //the properties I want to update
        //        project.TeamAssigned = project.TeamAssigned;
        //        project.Progress = projectUpdateDTO.Progress;
        //        // update assigned team
        //        await _context.SaveChangesAsync();

        //        return Ok($"Project with ID {id} updated successfully");
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProjectExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        [HttpPut("ChangeProjectTeam")]
        public async Task<bool> UpdateProjectTeam(int projectId, int newTeamId )
        {
            if (projectId == 0 || newTeamId == 0)
            {
                return false;
            }
            
            bool teamExists = await _context.Team.AnyAsync(t => t.Id == newTeamId);
            bool projectExists = await _context.Project.AnyAsync(p =>  p.Id == projectId);

            if (!teamExists || !projectExists)
            {
                return false;
            }

            try
            {
                var project = await _context.Project.FindAsync(projectId);

                if (project == null)
                {
                    return false;
                }

                project.TeamAssigned = newTeamId;
                await _context.SaveChangesAsync();
                return true; 
            }
            catch
            {
                return false;
            }
        }

        [HttpPut("UpdateProjectProgress")]
        public async Task<bool> UpdateProjectProgress(int projectId, int newProgress)
        {
            if (projectId == 0 || newProgress < 0 || newProgress > 100)
            {
                return false;
            }

            bool projectExists = await _context.Project.AnyAsync(p => p.Id == projectId);

            if (!projectExists)
            {
                return false;
            }

            try
            {
                var project = await _context.Project.FindAsync(projectId);

                if (project == null)
                {
                    return false;
                }

                project.Progress = newProgress;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
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

                var team = await _context.Team.FirstOrDefaultAsync(t => t.TeamName == projectCreateDTO.TeamAssigned);

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
                    TeamAssigned = team?.Id
                };

                // Save the project to the database
                _context.Project.Add(project);
                await _context.SaveChangesAsync();

                // After saving to the database, retrieve the created project
                var createdProject = await _context.Project.FindAsync(project.Id);

                // Convert to ProjectDTO before returning
                var createdProjectDTO = new ProjectDTO
                {
                    Id = createdProject.Id,
                    ClienName = projectCreateDTO.ClienName,
                    ClientProfileImg = projectCreateDTO.ClientProfileImg,
                    Project_Name = createdProject.Project_Name,
                    Description = createdProject.Description,
                    Project_Start = createdProject.Project_Start,
                    Duration_Week = createdProject.Duration_Week,
                    Project_Time = createdProject.Project_Time,
                    Project_Type = createdProject.Project_Type,
                    Project_Cost = createdProject.Project_Cost,
                    Amount_Paid = createdProject.Amount_Paid,
                    isCompleted = createdProject.isCompleted,
                    Progress = createdProject.Progress,
                    TeamAssigned = createdProject.TeamAssigned.ToString() // Convert the int? to a string
                };

                // Return the created ProjectDTO
                return CreatedAtAction("GetProject", new { id = createdProjectDTO.Id }, createdProjectDTO);
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
