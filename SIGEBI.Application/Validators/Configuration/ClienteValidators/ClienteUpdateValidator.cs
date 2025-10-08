using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.ClienteValidators
{
    public class ClienteValidator : IValidatorBase<ClienteUpdateDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(ClienteUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(ClienteUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
