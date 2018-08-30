using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.IoC.Tests.Validators
{

    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(10, 20)
                //.Custom((parent, x, resolver) =>
                //{
                //    return x.Length > 2;
                //})
                ;
        }
    }
}
