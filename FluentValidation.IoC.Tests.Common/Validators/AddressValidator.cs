using FluentValidation.IoC.Tests.Model;

namespace FluentValidation.IoC.Tests.Validators
{
    [DefaultValidator]
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.City).NotEmpty();

            RuleFor(x => x.PrimaryPhone)
                .WithDependencies()
                    .InjectValidator<PhoneValidator>();
                    //.Custom((parent, x, resolver) =>
                    //{
                    //    return true;
                    //});

            RuleForEach(x => x.OtherPhones)
                .WithDependencies()
                    .InjectValidator<PhoneValidator>();
        }
    }

    
    public class MainAddressValidator : AbstractValidator<Address>
    {
        public MainAddressValidator()
        {
            //Include(x => validatorProvider.GetValidator<Address>());
            RuleFor(x => x.PostCode).NotEmpty();
        }
    }
}
