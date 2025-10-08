using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.RolValidators
{
    public class RolCreateValidator : IValidatorBase<RolCreateDto>
    {
        public Task<ValidationResult> ValidateCreate(RolCreateDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(RolCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
