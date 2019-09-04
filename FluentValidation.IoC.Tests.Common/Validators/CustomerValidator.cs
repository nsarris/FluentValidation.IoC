using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Services;

namespace FluentValidation.IoC.Tests.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().ResolveName();
            RuleFor(x => x.Name).NotEmpty().ResolveName();
            RuleFor(x => x.VatNumber)
                .WithDependencies()
                    .Inject<IVatService>()
                    .Must((customer, vatNumber, vatService) =>
                    {
                        return vatService.IsValid(vatNumber);
                    })
                .WithErrorCode("VatValidationServiceFailure")
                .ResolveMessage();


            RuleFor(x => x.MainAddress)
                .NotNull()
                .ResolveName()
                .WithDependencies()
                    .InjectValidator<MainAddressValidator>();
        }
    }
}
