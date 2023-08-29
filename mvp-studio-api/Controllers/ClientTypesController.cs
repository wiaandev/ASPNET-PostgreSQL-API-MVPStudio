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
    public class ClientTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ClientTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientType>>> GetClient_Type()
        {
          if (_context.Client_Type == null)
          {
              return NotFound();
          }
            return await _context.Client_Type.ToListAsync();
        }

        // GET: api/ClientTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientType>> GetClientType(int id)
        {
          if (_context.Client_Type == null)
          {
              return NotFound();
          }
            var clientType = await _context.Client_Type.FindAsync(id);

            if (clientType == null)
            {
                return NotFound();
            }

            return clientType;
        }

        // PUT: api/ClientTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientType(int id, ClientType clientType)
        {
            if (id != clientType.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientTypeExists(id))
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

        // POST: api/ClientTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientType>> PostClientType(ClientType clientType)
        {
          if (_context.Client_Type == null)
          {
              return Problem("Entity set 'AppDbContext.Client_Type'  is null.");
          }
            _context.Client_Type.Add(clientType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientType", new { id = clientType.Id }, clientType);
        }

        // DELETE: api/ClientTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientType(int id)
        {
            if (_context.Client_Type == null)
            {
                return NotFound();
            }
            var clientType = await _context.Client_Type.FindAsync(id);
            if (clientType == null)
            {
                return NotFound();
            }

            _context.Client_Type.Remove(clientType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientTypeExists(int id)
        {
            return (_context.Client_Type?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
