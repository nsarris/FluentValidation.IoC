using System;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Services;
using FluentValidation.IoC.Tests.Validators;
using NUnit.Framework;
using Unity;
using Unity.Lifetime;
using FluentValidation.IoC.Unity;
using Microsoft.Extensions.DependencyInjection;

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

            //TODO: Scoped container?

            container
                .RegisterFluentValidation(configure => configure
                    .UsingAssemblyScanner(scanner => scanner.Scan(TestHelper.GetLoadedUserAssemblies()))
                    .WithDuplicateResolutionByAttribute<DefaultValidatorAttribute>()
                    .AddLiteralService<MockLiteralService>(ServiceLifetime.Singleton)
                    )

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
