using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.IoC.Tests.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.IoC.Tests.Validators
{

    public class PhoneValidator : AbstractValidator<Phone>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Number)
                .NotEmpty()
                .Length(10, 20)
                .ResolveName()
                .Custom((x, context) =>
                {
                    var serviceProvider = context.GetServiceProvider();
                    var parent = context.InstanceToValidate;
                    
                    var phoneBookService = serviceProvider.GetRequiredService<IPhoneBookService>();
                    if (!phoneBookService.IsExistingNumber(x))
                        context.AddFailure($"{x} is not an existing phone number");

                    if ((parent.Kind == PhoneKind.Home || parent.Kind == PhoneKind.Work)
                        && !phoneBookService.IsLandline(x))
                            context.AddFailure($"{x} is not a valid landline phone number");
                    else if (parent.Kind == PhoneKind.Mobile
                        && !phoneBookService.IsMobile(x))
                            context.AddFailure($"{x} is not a valid mobile phone number");
                });
        }
    }
}
