using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        // GET: api/<RolController>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            ServiceResult result = await _rolService.GetRolAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<RolController>/5
        [HttpGet("GetEntityByID")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult result = await _rolService.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST api/<RolController>
        [HttpPost("create-rol")]
        public async Task<IActionResult> Post([FromBody] RolCreateDto rolCreateDto)
        {
            ServiceResult result = await _rolService.CreateRol(rolCreateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<RolController>/5
        [HttpPut("update-rol")]
        public async Task<IActionResult> Put([FromBody] RolUpdateDto rolUpdateDto)
        {
            ServiceResult result = await _rolService.UpdateRol(rolUpdateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("remove_rol")]
        public async Task<IActionResult> Delete([FromBody] RolRemoveDto rolRemoveDto)
        {
            ServiceResult result = new ServiceResult();
            result = await _rolService.RemoveRol(rolRemoveDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
