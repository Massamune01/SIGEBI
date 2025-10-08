using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.ClienteValidators
{
    public class ClienteCreateValidator : IValidatorBase<ClienteCreateDto>
    {
        public Task<ValidationResult> ValidateCreate(ClienteCreateDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(ClienteCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
