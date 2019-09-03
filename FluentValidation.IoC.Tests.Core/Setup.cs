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
                .AddFluentValidation(configure => configure
                    .UsingAssemblyScanner(scanner => scanner.Scan(TestHelper.GetLoadedUserAssemblies()))
                    .WithDuplicateResolutionByAttribute<DefaultValidatorAttribute>()
                    .AddLiteralService<MockLiteralService>(ServiceLifetime.Singleton)
                    )

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
