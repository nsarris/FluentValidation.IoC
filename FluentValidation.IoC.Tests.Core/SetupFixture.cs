using FluentValidation.IoC.Tests.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC.Tests.Core
{
    public class SetupFixture
    {
        private readonly Lazy<IServiceProvider> serviceProvider = new Lazy<IServiceProvider>(() => BuildServiceProvider());

        public IServiceProvider ServiceProvider
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
    }
}
