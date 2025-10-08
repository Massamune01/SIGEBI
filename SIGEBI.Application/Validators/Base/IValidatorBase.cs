using SIGEBI.Application.Base;

namespace SIGEBI.Application.Validators.Base
{
    public interface IValidatorBase<T>
    {
        Task<ValidationResult> ValidateCreate(T entity);
        Task<ValidationResult> ValidateUpdate(T entity);
    }
}

