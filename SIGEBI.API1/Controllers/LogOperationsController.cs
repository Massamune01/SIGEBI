using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogOperationsController : ControllerBase
    {
        private readonly SIGEBIContext _context;

        public LogOperationsController(SIGEBIContext context)
        {
            _context = context;
        }

        // GET: api/LogOperations
        [HttpGet("GetAllCrede")]
        public async Task<ActionResult<IEnumerable<LogOperations>>> GetLogOperations()
        {
            return await _context.LogOperations.ToListAsync();
        }

        // GET: api/LogOperations/5
        [HttpGet("GetCredeById")]
        public async Task<ActionResult<LogOperations>> GetLogOperations(int id)
        {
            var logOperations = await _context.LogOperations.FindAsync(id);

            if (logOperations == null)
            {
                return NotFound();
            }

            return logOperations;
        }

        // PUT: api/LogOperations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-credenciales")]
        public async Task<IActionResult> PutLogOperations(int id, LogOperations logOperations)
        {
            if (id != logOperations.Id)
            {
                return BadRequest();
            }

            _context.Entry(logOperations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogOperationsExists(id))
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

        // POST: api/LogOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-credenciales")]
        public async Task<ActionResult<LogOperations>> PostLogOperations(LogOperations logOperations)
        {
            _context.LogOperations.Add(logOperations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogOperations", new { id = logOperations.Id }, logOperations);
        }

        // DELETE: api/LogOperations/5
        [HttpDelete("remove-credenciales")]
        public async Task<IActionResult> DeleteLogOperations(int id)
        {
            var logOperations = await _context.LogOperations.FindAsync(id);
            if (logOperations == null)
            {
                return NotFound();
            }

            _context.LogOperations.Remove(logOperations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogOperationsExists(int id)
        {
            return _context.LogOperations.Any(e => e.Id == id);
        }
    }
}
