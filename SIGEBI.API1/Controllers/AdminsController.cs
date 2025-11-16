using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/Admins
        [HttpGet("GetAllAdmin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            ServiceResult result = await _adminService.GetAllAdminAsync();

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/Admins/5
        [HttpGet("GetAdminById")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            ServiceResult result = await _adminService.GetAdminByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> PutAdmin(AdminUpdateDto adminUpdateDto)
        {
            ServiceResult result = await _adminService.UpdateAdminAsync(adminUpdateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-admin")]
        public async Task<ActionResult<Admin>> PostAdmin(AdminCreateDto adminCreateDto)
        {
            ServiceResult result = await _adminService.CreateAdminAsync(adminCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // DELETE: api/Admins/5
        [HttpDelete("remove-admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            ServiceResult result = await _adminService.DeleteAdminAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
