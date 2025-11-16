using SIGEBI.Application.Base;

namespace SIGEBI.Application.Validators.Base
{
    public interface IValidatorBase<T>
    {
        Task<ValidationResult> Validate(T entity, int opcion);
    }
}

