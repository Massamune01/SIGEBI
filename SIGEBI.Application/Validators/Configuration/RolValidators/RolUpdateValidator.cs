using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.RolValidators
{
    public class RolUpdateValidator : IValidatorBase<RolUpdateDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(RolUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(RolUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
