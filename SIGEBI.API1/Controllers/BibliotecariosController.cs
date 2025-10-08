using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecariosController : ControllerBase
    {
        private readonly SIGEBIContext _context;

        public BibliotecariosController(SIGEBIContext context)
        {
            _context = context;
        }

        // GET: api/Bibliotecarios
        [HttpGet("GetAllBiblio")]
        public async Task<ActionResult<IEnumerable<Bibliotecarios>>> GetBibliotecarios()
        {
            return await _context.Bibliotecarios.ToListAsync();
        }

        // GET: api/Bibliotecarios/5
        [HttpGet("GetBiblioById")]
        public async Task<ActionResult<Bibliotecarios>> GetBibliotecarios(int id)
        {
            var bibliotecarios = await _context.Bibliotecarios.FindAsync(id);

            if (bibliotecarios == null)
            {
                return NotFound();
            }

            return bibliotecarios;
        }

        // PUT: api/Bibliotecarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-Biblio")]
        public async Task<IActionResult> PutBibliotecarios(int id, Bibliotecarios bibliotecarios)
        {
            if (id != bibliotecarios.Id)
            {
                return BadRequest();
            }

            _context.Entry(bibliotecarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BibliotecariosExists(id))
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

        // POST: api/Bibliotecarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-biblio")]
        public async Task<ActionResult<Bibliotecarios>> PostBibliotecarios(Bibliotecarios bibliotecarios)
        {
            _context.Bibliotecarios.Add(bibliotecarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBibliotecarios", new { id = bibliotecarios.Id }, bibliotecarios);
        }

        // DELETE: api/Bibliotecarios/5
        [HttpDelete("remove-biblio")]
        public async Task<IActionResult> DeleteBibliotecarios(int id)
        {
            var bibliotecarios = await _context.Bibliotecarios.FindAsync(id);
            if (bibliotecarios == null)
            {
                return NotFound();
            }

            _context.Bibliotecarios.Remove(bibliotecarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BibliotecariosExists(int id)
        {
            return _context.Bibliotecarios.Any(e => e.Id == id);
        }
    }
}
