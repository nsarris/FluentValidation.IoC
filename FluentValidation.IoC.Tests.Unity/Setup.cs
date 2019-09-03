using System;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Services;
using FluentValidation.IoC.Tests.Validators;
using NUnit.Framework;
using Unity;
using Unity.Lifetime;
using FluentValidation.IoC.Unity;

namespace FluentValidation.IoC.Tests
{
    [SetUpFixture]
    public class Setup
    {
        public static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() => BuildContainer());

        public static IUnityContainer Container => container.Value;

        public static IUnityContainer BuildContainer()
        {
            var container = new UnityContainer();

            //This will make sure each IoCValidationContext gets a child container
            //so disposing it wont dispose the base container
            container
                //Register container as an IServiceProvider
                //.RegisterAsServiceProvider()
                //Register the ValidationContextProvider
                //.RegisterValidationContextProvider()
                //Register container as a default validator factory
                //.RegisterDefaultValidatorFactory()
                //Register all validators in assemblies as singletons
                //.RegisterAllValidators()
                
                .RegisterFluentValidation(configure => configure
                    .UsingAssemblyScanner(scanner => scanner.Scan(AppDomain.CurrentDomain.GetAssemblies().FilterFramework()))
                    //.WithDuplicateResolution(x => x.OrderBy(y => y.ImplementationType.GetCustomAttributes(false).Any(a => a.GetType() == typeof(DefaultValidatorAttribute))).First())
                    .WithDuplicateResolutionByAttribute<DefaultValidatorAttribute>()
                    )

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

            return container;
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            //Container = BuildContainer();
        }
    }
}
