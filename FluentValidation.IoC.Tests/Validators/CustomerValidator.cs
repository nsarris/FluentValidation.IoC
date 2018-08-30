using FluentValidation.IoC.Tests.Model;

namespace FluentValidation.IoC.Tests.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty().ResolveName();
            RuleFor(x => x.Name).NotEmpty().ResolveName();
            RuleFor(x => x.VatNumber)
                .WithIoC()
                .Using<IVatService>()
                .Custom((customer, vatNumber, vatService) =>
                {
                    return vatService.IsValid(vatNumber);
                })
                .ResolveMessage("VatValidationServiceFailure");


            RuleFor(x => x.MainAddress)
                .NotNull()
                .ResolveName()
                .WithIoC()
                    .SetValidator<MainAddressValidator>();
        }
    }
}
