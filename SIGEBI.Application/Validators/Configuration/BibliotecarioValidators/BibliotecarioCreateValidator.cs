using SIGEBI.Application.Base;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Validators.Configuration.BibliotecarioValidators
{
    public class BibliotecarioCreateValidator : IValidatorBase<BibliotecarioCreateValidator>
    {
        //[("Unica implementacion")]
        public Task<ValidationResult> ValidateCreate(BibliotecarioCreateValidator entity)
        {
            throw new NotImplementedException();
        }


        //No se implementa
        public Task<ValidationResult> ValidateUpdate(BibliotecarioCreateValidator entity)
        {
            throw new NotImplementedException();
        }
    }
}
