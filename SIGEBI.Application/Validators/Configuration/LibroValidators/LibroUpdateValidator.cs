using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.LibroValidators
{
    public class LibroUpdateValidator : IValidatorBase<LibroUpdateDto>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(LibroUpdateDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ValidationResult> ValidateUpdate(LibroUpdateDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
