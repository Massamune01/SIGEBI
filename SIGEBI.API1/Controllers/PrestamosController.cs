using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamosService _prestamosService;

        public PrestamosController(IPrestamosService prestamosService)
        {
            _prestamosService = prestamosService;
        }

        // GET: api/Prestamos
        [HttpGet("GetAllPrest")]
        public async Task<ActionResult<IEnumerable<Prestamos>>> GetPrestamos()
        {
            ServiceResult result = await _prestamosService.GetAllPrestamosAsync();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // GET: api/Prestamos/5
        [HttpGet("GetPrestById")]
        public async Task<ActionResult<Prestamos>> GetPrestamos(int id)
        {
            ServiceResult result = await _prestamosService.GetPrestamoByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // PUT: api/Prestamos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-prestamo")]
        public async Task<IActionResult> PutPrestamos(PrestamoUpdateDto prestamoUpdateDto)
        {
            ServiceResult result = await _prestamosService.UpdatePrestamoAsync(prestamoUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // POST: api/Prestamos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-prestamos")]
        public async Task<ActionResult<Prestamos>> PostPrestamos(PrestamoCreateDto prestamoCreateDto)
        {
            ServiceResult result = await _prestamosService.CreatePrestamoAsync(prestamoCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // DELETE: api/Prestamos/5
        [HttpDelete("remove-prestamo")]
        public async Task<IActionResult> DeletePrestamos(int id)
        {
            ServiceResult result = await _prestamosService.DeletePrestamoAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
