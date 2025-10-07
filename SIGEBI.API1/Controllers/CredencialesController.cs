using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        private readonly ICredencialesService _credencialesService;

        public CredencialesController(ICredencialesService credencialesService)
        {
            _credencialesService = credencialesService;
        }

        // GET: api/<CredencialesController>
        [HttpGet("GetCredenciales")]
        public async Task<IActionResult> Get()
        {
            ServiceResult result = await _credencialesService.GetCredencialesAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<CredencialesController>/5
        [HttpGet("GetEntityByID")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult result = await _credencialesService.GetCredencialesById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST api/<CredencialesController>
        [HttpPost("create-credenciales")]
        public async Task<IActionResult> Post([FromBody] CredencialesCreateDto credencialesCreateDto)
        {
            ServiceResult result = await _credencialesService.CreateCredenciales(credencialesCreateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<CredencialesController>/5
        [HttpPut("update-credenciales")]
        public async Task<IActionResult> Put([FromBody] CredencialesUpdateDto credencialesUpdateDto)
        {
            ServiceResult result = await _credencialesService.UpdateCredenciales(credencialesUpdateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE api/<CredencialesController>/5
        [HttpDelete("remove-credenciales")]
        public async Task<IActionResult> Delete(CredencialesRemoveDto credencialesRemoveDto)
        {
            ServiceResult result = await _credencialesService.RemoveCredenciales(credencialesRemoveDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
