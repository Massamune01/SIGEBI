using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Validators.Configuration.LogOpValidators
{
    public class LogOperationUpdateValidator : IValidatorBase<UpdateLogOperationDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(UpdateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(UpdateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
