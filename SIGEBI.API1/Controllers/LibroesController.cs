using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroesController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibroesController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        // GET: api/Libro
        [HttpGet("GetAllLibros")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            ServiceResult result = await _libroService.GetAllLibrosAsync();

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // GET: api/Libro/5
        [HttpGet("GetLibroById")]
        public async Task<ActionResult<Libro>> GetLibro(Int64 id)
        {
            ServiceResult result = await _libroService.GetLibroByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // PUT: api/Libroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-libro")]
        public async Task<IActionResult> PutLibro(LibroUpdateDto libroUpdateDto)
        {
            ServiceResult result = await _libroService.UpdateLibroAsync(libroUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // POST: api/Libroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-libro")]
        public async Task<ActionResult<Libro>> PostLibro(LibroCreateDto libroCreateDto)
        {
            ServiceResult result = await _libroService.CreateLibroAsync(libroCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // DELETE: api/Libroes/5
        [HttpDelete("remove-libro")]
        public async Task<IActionResult> DeleteLibro(Int64 id)
        {
            ServiceResult result = await _libroService.DeleteLibroAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
