using FluentValidation.IoC.Tests.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC.Tests.Core
{
    [SetUpFixture]
    public class Setup
    {
        private static Lazy<IServiceProvider> serviceProvider = new Lazy<IServiceProvider>(() => BuildServiceProvider());

        public static IServiceProvider ServiceProvider //{ get; private set; }
            => serviceProvider.Value;


        private static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                //Register the ValidationContextProvider
                //.AddValidationContextProvider()
                //Register container as a default validator factory
                //.AddDefaultValidatorProvider()
                //Register all validators in assemblies as singletons
                //.AddValidators()

                .AddFluentValidation(configure => configure
                    .UsingAssemblyScanner(scanner => scanner.Scan(AppDomain.CurrentDomain.GetAssemblies().FilterFramework()))
                    //.WithDuplicateResolution(x => x.OrderBy(y => y.ImplementationType.GetCustomAttributes(false).Any(a => a.GetType() == typeof(DefaultValidatorAttribute))).First())
                    .WithDuplicateResolutionByAttribute<DefaultValidatorAttribute>()
                    )

                //Register Literal Service
                .AddLiteralService<MockLiteralService>(ServiceLifetime.Singleton)

                //Register business services
                .AddTransient<IVatService, MockVatService>()
                .AddTransient<IPhoneBookService, MockPhoneBookService>()

            .BuildServiceProvider();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            //ServiceProvider = BuildServiceProvider();
        }
    }
}
