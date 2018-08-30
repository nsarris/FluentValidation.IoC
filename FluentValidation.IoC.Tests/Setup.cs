using System;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Services;
using FluentValidation.IoC.Tests.Validators;
using FluentValidation.IoC.Unity;
using NUnit.Framework;
using Unity;

namespace FluentValidation.IoC.Tests
{
    [SetUpFixture]
    public class Setup
    {
        public static UnityContainer Container { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            var container = new UnityContainer();

            //This will make sure each IoCValidationContext gets a child container
            //so disposing it wont dispose the base container
            container.RegisterResolverAndFactory<UnityValidatorHierarchicalResolver>();

            //This will make sure each IoCValidationContext gets a shared container which it doesn't dispose
            //when the context is disposed
            //container.RegisterResolverAndFactory<UnityValidatorResolver>();

            //Using the following resolver will dispose the container after each resolution context is disposed
            //This only usefull if the container used is also injected as a dependency (e.g. child by factory)
            //container.RegisterResolverAndFactory<UnityValidatorDisposableResolver>();

            //Registration by conenvtion is broken in .net framework in Unit.Registration by convention
            //From version 2.1.6 up to 2.1.8 (current)
            //container.RegisterAllValidatorsAsSingletons(new[] { this.GetType().Assembly });

            container.RegisterType<IValidator<Customer>, CustomerValidator>();
            container.RegisterType<IValidator<Address>, AddressValidator>();
            container.RegisterType<IValidator<Phone>, PhoneValidator>();

            container.RegisterType<IVatService, MockVatService>();
            container.RegisterType<IPhoneBookService, MockPhoneBookService>();

            ServiceLocator.LiteralService = new MockLiteralService();

            Container = container;
        }
    }
}
