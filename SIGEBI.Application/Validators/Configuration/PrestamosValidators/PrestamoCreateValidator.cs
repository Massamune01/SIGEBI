using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Validators.Configuration.PrestamosValidators
{
    public class PrestamoCreateValidator : IValidatorBase<PrestamoCreateDto>
    {
        public Task<ValidationResult> ValidateCreate(PrestamoCreateDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(PrestamoCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
