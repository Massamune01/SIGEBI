using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.LibroValidators
{
    public class LibroCreateValidator : IValidatorBase<LibroCreateDto>
    {
        public Task<ValidationResult> ValidateCreate(LibroCreateDto entity)
        {
            throw new NotImplementedException();
        }

        //No se implementa
        public Task<ValidationResult> ValidateUpdate(LibroCreateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
