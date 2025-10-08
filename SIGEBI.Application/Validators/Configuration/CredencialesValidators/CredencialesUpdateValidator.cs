using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.CredencialesDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.CredencialesValidators
{
    public class CredencialesUpdateValidator : IValidatorBase<CredencialesUpdateDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(CredencialesUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(CredencialesUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
