using System;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Services;
using FluentValidation.IoC.Tests.Validators;
using FluentValidation.IoC.Unity;
using NUnit.Framework;
using Unity;
using Unity.Lifetime;

namespace FluentValidation.IoC.Tests
{
    [SetUpFixture]
    public class Setup
    {
        public static IUnityContainer Container { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            var container = new UnityContainer();

            //This will make sure each IoCValidationContext gets a child container
            //so disposing it wont dispose the base container
            container
                //Register container as an IServiceProvider
                .RegisterAsServiceProvider()
                //Register the ValidationContextProvider
                .RegisterValidationContextProvider()
                //Register container as a default validator factory
                .RegisterDefaultValidatorFactory()
                //Register all validators in assemblies as singletons
                .RegisterAllValidators()

            //Registration from specific assembly
            //  .RegisterAllValidators(new[] { this.GetType().Assembly });

            //Manual registration
            //  .RegisterType<IValidator<Customer>, CustomerValidator>();
            //  .RegisterType<IValidator<Address>, AddressValidator>();
            //  .RegisterType<IValidator<Phone>, PhoneValidator>();

            //Register Literal Service
                .RegisterLiteralService<MockLiteralService>(new SingletonLifetimeManager())

            //Register business services
                .RegisterType<IVatService, MockVatService>()
                .RegisterType<IPhoneBookService, MockPhoneBookService>();

            Container = container;
        }
    }
}
