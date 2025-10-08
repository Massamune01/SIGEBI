using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Application.Validators.Configuration.PrestamosValidators
{
    public class PrestamoUpdateValidator : IValidatorBase<PrestamoUpdateDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(PrestamoUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(PrestamoUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
