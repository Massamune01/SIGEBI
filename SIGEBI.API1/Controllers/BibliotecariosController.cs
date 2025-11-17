using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecariosController : ControllerBase
    {
        private readonly IBibliotecarioService _bibliotecarioService;

        public BibliotecariosController(IBibliotecarioService bibliotecarioService)
        {
            _bibliotecarioService = bibliotecarioService;
        }

        // GET: api/Bibliotecarios
        [HttpGet("GetAllBiblio")]
        public async Task<ActionResult<IEnumerable<Bibliotecarios>>> GetBibliotecarios()
        {
            ServiceResult result = await _bibliotecarioService.GetAllBibliotecariosAsync();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/Bibliotecarios/5
        [HttpGet("GetBiblioById")]
        public async Task<ActionResult<Bibliotecarios>> GetBibliotecarios(int id)
        {
            ServiceResult result = await _bibliotecarioService.GetBibliotecarioByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // PUT: api/Bibliotecarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-Biblio")]
        public async Task<IActionResult> PutBibliotecarios(BibliotecarioUpdateDto bibliotecarioUpdateDto)
        {
            ServiceResult result = await _bibliotecarioService.UpdateBibliotecarioAsync(bibliotecarioUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST: api/Bibliotecarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-biblio")]
        public async Task<ActionResult<Bibliotecarios>> PostBibliotecarios(BibliotecarioCreateDto bibliotecarioCreateDto)
        {
            ServiceResult result = await _bibliotecarioService.CreateBibliotecarioAsync(bibliotecarioCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // DELETE: api/Bibliotecarios/5
        [HttpDelete("remove-biblio")]
        public async Task<IActionResult> DeleteBibliotecarios(int id)
        {
            ServiceResult result = await _bibliotecarioService.DeleteBibliotecarioAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
