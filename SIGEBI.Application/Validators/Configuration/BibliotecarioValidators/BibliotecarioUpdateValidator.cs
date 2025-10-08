using SIGEBI.Application.Base;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.BibliotecarioValidators
{
    public class BibliotecarioUpdateValidator : IValidatorBase<BibliotecarioUpdateValidator>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(BibliotecarioUpdateValidator entity)
        {
            throw new NotImplementedException();
        }

        //("Implementacion requerida")
        public Task<ValidationResult> ValidateUpdate(BibliotecarioUpdateValidator entity)
        {
            throw new NotImplementedException();
        }
    }
}
