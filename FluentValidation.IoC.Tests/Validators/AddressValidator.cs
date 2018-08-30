using FluentValidation.IoC.Tests.Model;

namespace FluentValidation.IoC.Tests.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.City).NotEmpty();

            RuleFor(x => x.PrimaryPhone)
                .WithIoC()
                    .SetValidator<PhoneValidator>();
                    //.Custom((parent, x, resolver) =>
                    //{
                    //    return true;
                    //});

            RuleForEach(x => x.OtherPhones)
                .WithIoC()
                    .SetValidator<PhoneValidator>();
        }
    }

    public class MainAddressValidator : AbstractValidator<Address>
    {
        public MainAddressValidator(IValidatorFactory validatorFactory)
        {
            Include(x => validatorFactory.GetValidator<Address>());
            RuleFor(x => x.PostCode).NotEmpty();
        }
    }
}
