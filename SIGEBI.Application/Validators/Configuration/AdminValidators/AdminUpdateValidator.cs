using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Application.Base;
using SIGEBI.Application.Validators.Base;

namespace SIGEBI.Application.Validators.Configuration.AdminValidators
{
    public class AdminUpdateValidator : IValidatorBase<AdminUpdateValidator>
    {
        //No se implementa
        public Task<ValidationResult> ValidateCreate(AdminUpdateValidator entity)
        {
            throw new NotImplementedException();
        }

        //[("Implementacion requerida")]
        public Task<ValidationResult> ValidateUpdate(AdminUpdateValidator entity)
        {
            throw new NotImplementedException();
        }
    }
}
