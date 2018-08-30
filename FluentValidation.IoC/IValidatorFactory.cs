using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    public interface IValidatorFactory : FluentValidation.IValidatorFactory
    {
        TValidator GetValidator<T, TValidator>() where TValidator : IValidator<T>;
        IValidator<T> GetValidator<T>(Type validatorType);
    }
}
