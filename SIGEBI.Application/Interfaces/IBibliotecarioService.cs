using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;

namespace SIGEBI.Application.Interfaces
{
    public interface IBibliotecarioService
    {
        Task<ServiceResult> GetAllBibliotecariosAsync();
        Task<ServiceResult> GetBibliotecarioByIdAsync(int id);
        Task<ServiceResult> CreateBibliotecarioAsync(BibliotecarioCreateDto bibliotecarioCreateDto);
        Task<ServiceResult> UpdateBibliotecarioAsync(BibliotecarioUpdateDto bibliotecarioUpdateDto);
        Task<ServiceResult> DeleteBibliotecarioAsync(int id);
    }
}
