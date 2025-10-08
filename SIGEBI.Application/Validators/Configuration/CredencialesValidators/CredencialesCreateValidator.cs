using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.CredencialesValidators
{
    public class CredencialesCreateValidator : IValidatorBase<CredencialesCreateDto>
    {
        public Task<ValidationResult> ValidateCreate(CredencialesCreateDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(CredencialesCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
