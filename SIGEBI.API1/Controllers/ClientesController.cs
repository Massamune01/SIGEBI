using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Services;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet("GetAllClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            ServiceResult result = await _clienteService.GetAllClientesAsync();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // GET: api/Clientes/5
        [HttpGet("GetClienteById")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            ServiceResult result = await _clienteService.GetClienteByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update-cliente")]
        public async Task<IActionResult> PutCliente(ClienteUpdateDto clienteUpdateDto)
        {
            ServiceResult result = await _clienteService.UpdateClienteAsync(clienteUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create-cliente")]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteCreateDto clienteCreateDto)
        {
            ServiceResult result = await _clienteService.CreateClienteAsync(clienteCreateDto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("remove-cliente")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            ServiceResult result = await _clienteService.DeleteClienteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
