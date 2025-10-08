using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Base;

namespace SIGEBI.Application.Validators.Configuration.LogOpValidators
{
    public class LogOperationCreateValidator : IValidatorBase<CreateLogOperationDto>
    {
       
        public Task<ValidationResult> ValidateCreate(CreateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(CreateLogOperationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
