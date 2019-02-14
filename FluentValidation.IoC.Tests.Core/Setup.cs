using FluentValidation.IoC.Tests.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC.Tests.Core
{
    [SetUpFixture]
    public class Setup
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            ServiceProvider = new ServiceCollection()
                //Register the ValidationContextProvider
                .AddValidationContextProvider()
                //Register container as a default validator factory
                .AddDefaultValidatorFactory()
                //Register all validators in assemblies as singletons
                .AddValidators()
                //Register Literal Service
                .AddLiteralService<MockLiteralService>(ServiceLifetime.Singleton)

                //Register business services
                .AddTransient<IVatService, MockVatService>()
                .AddTransient<IPhoneBookService, MockPhoneBookService>()

            .BuildServiceProvider();
        }
    }
}
