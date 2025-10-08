using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ServiceResult> GetAllClientesAsync();
        Task<ServiceResult> GetClienteByIdAsync(int id);
        Task<ServiceResult> CreateClienteAsync(ClienteCreateDto clienteCreateDto);
        Task<ServiceResult> UpdateClienteAsync(ClienteUpdateDto clienteUpdateDto);
        Task<ServiceResult> DeleteClienteAsync(int id);
    }
}
