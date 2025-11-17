using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogOperationsController : ControllerBase
    {
        private readonly ILogOperationsService _logOperationsService;

        public LogOperationsController(ILogOperationsService logOperationsService)
        {
            _logOperationsService = logOperationsService;
        }

        // GET: api/LogOperations
        [HttpGet("GetAllLogOp")]
        public async Task<ActionResult<IEnumerable<LogOperations>>> GetLogOperations()
        {
            ServiceResult result = await _logOperationsService.GetAllLogOperationsAsync();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // GET: api/LogOperations/5
        [HttpGet("GetLogOpById")]
        public async Task<ActionResult<LogOperations>> GetLogOperations(int id)
        {
            ServiceResult result = await _logOperationsService.GetLogOperationsByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // PUT: api/LogOperations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-LogOp")]
        public async Task<IActionResult> PutLogOperations(UpdateLogOperationDto logOperationUpdateDto)
        {
            ServiceResult result = await _logOperationsService.UpdateLogOperationsAsync(logOperationUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // POST: api/LogOperations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-LogOp")]
        public async Task<ActionResult<LogOperations>> PostLogOperations(CreateLogOperationDto logOperationsCreateDto)
        {
            ServiceResult result = await _logOperationsService.CreateLogOperationsAsync(logOperationsCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        // DELETE: api/LogOperations/5
        [HttpDelete("remove-LogOp")]
        public async Task<IActionResult> DeleteLogOperations(int id)
        {
            ServiceResult result = await _logOperationsService.DeleteLogOperationsAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
