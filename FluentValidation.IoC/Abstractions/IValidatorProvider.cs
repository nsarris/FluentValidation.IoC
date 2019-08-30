using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public interface IValidatorProvider : FluentValidation.IValidatorFactory
    {
        IValidator GetSpecificValidator(Type validatorType);
    }
}
