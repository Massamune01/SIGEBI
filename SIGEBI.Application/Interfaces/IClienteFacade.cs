using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IClienteFacade
    {
        Task<ServiceResult> CreateClienteAsync(ClienteCreateDto clienteCreateDto);
        Task<ServiceResult> UpdateClienteAsync(ClienteUpdateDto clienteUpdateDto);
        Task<ServiceResult> DeleteClienteAsync(int id);
        Task<ServiceResult> GetClienteByIdAsync(int id);
        Task<ServiceResult> GetAllClienteAsync();
    }
}
