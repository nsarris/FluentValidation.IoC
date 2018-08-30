using System;
using FluentValidation.IoC.Tests.Model;
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

            container.RegisterResolverAndFactory<UnityValidatorHierarchicalResolver>();

            //Registration by conenvtion is broken in .net framework in Unit.Registration by convention
            //From version 2.1.6 up to 2.1.8 (current)
            //container.RegisterAllValidatorsAsSingletons(new[] { this.GetType().Assembly });

            container.RegisterType<IValidator<Customer>, CustomerValidator>();
            container.RegisterType<IValidator<Address>, AddressValidator>();
            container.RegisterType<IValidator<Phone>, PhoneValidator>();

            container.RegisterType<IVatService, MockVatService>();

            ServiceLocator.LiteralService = new MockLiteralService();

            Container = container;
        }
    }
}
