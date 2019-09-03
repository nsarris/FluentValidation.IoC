using FluentValidation.IoC;
using FluentValidation.IoC.Tests;
using FluentValidation.IoC.Tests.Core;
using FluentValidation.IoC.Tests.Model;
using FluentValidation.IoC.Tests.Validators;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FluentValidation.IoC.Tests.Core
{
    public class ConfigurationTests : IClassFixture<SetupFixture>
    {
        private readonly SetupFixture fixture;

        public ConfigurationTests(SetupFixture fixture)
        {
            this.fixture = fixture;
        }

        private bool IsValidatorInterface(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IValidator<>);
        }

        private bool IsValidatorType(Type type)
        {
            return type.IsInterface ? IsValidatorInterface(type) : type.GetInterfaces().Any(IsValidatorInterface);
        }

        private Type GetValidatorInterface(Type validatorType)
        {
            return validatorType.GetInterfaces().FirstOrDefault(IsValidatorInterface);
        }

        [Theory]
        [InlineData(typeof(IValidationContextProvider), typeof(ValidationContextProvider))]
        [InlineData(typeof(IValidationContextProvider<>), typeof(ValidationContextProvider<>))]
        [InlineData(typeof(IValidationContextProvider<,>), typeof(ValidationContextProvider<,>))]
        public void Should_Add_ValidationContextProvider(Type serviceType, Type implementationType)
        {
            var services = new ServiceCollection()
               .AddFluentValidation();

            Assert.Contains(services, 
                x => x.ServiceType == serviceType
                && x.ImplementationType == implementationType
                && x.Lifetime == ServiceLifetime.Transient
                );
        }

        [Fact]
        public void Should_Add_Default_ValidatorProvider()
        {
            var services = new ServiceCollection()
               .AddFluentValidation();

            var defaultConfiguration = new FluentValidationConfiguration(services);

            Assert.Contains(services,
                x => x.ServiceType == typeof(DefaultValidatorProvider)
                && x.ImplementationType == defaultConfiguration.ValidatorProviderServiceDescriptor.ImplementationType
                && x.Lifetime == defaultConfiguration.ValidatorProviderServiceDescriptor.Lifetime
                );

            Assert.Contains(services,
                x => x.ServiceType == typeof(IValidatorProvider)
                && x.ImplementationType == null
                && x.ImplementationFactory != null
                && x.Lifetime == defaultConfiguration.ValidatorProviderServiceDescriptor.Lifetime
                );
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(ServiceLifetime.Singleton)]
        [InlineData(ServiceLifetime.Transient)]
        public void Should_Add_Custom_ValidatorProvider(ServiceLifetime serviceLifetime)
        {
            var validatorProviderType = new Mock<IValidatorProvider>().Object.GetType();

            var services = new ServiceCollection()
               .AddFluentValidation(config => config
                    .AddValidatorProvider(validatorProviderType, serviceLifetime));

            Assert.Contains(services,
                x => x.ServiceType == validatorProviderType
                && x.ImplementationType == validatorProviderType
                && x.Lifetime == serviceLifetime
                );

            Assert.Contains(services,
                x => x.ServiceType == typeof(IValidatorProvider)
                && x.ImplementationType == null
                && x.ImplementationFactory != null
                && x.Lifetime == serviceLifetime
                );
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped, false)]
        [InlineData(ServiceLifetime.Singleton, false)]
        [InlineData(ServiceLifetime.Transient, false)]
        [InlineData(ServiceLifetime.Scoped, true)]
        [InlineData(ServiceLifetime.Singleton, true)]
        [InlineData(ServiceLifetime.Transient, true)]
        public void Should_Add_Scanned_Validators(ServiceLifetime serviceLifetime, bool mapInterfaces)
        {
            var validatorTypes = new[] { typeof(CustomerValidator), typeof(PhoneValidator) };

            var assemblyScanner = new Mock<IAssemblyScanner>();

            assemblyScanner
                .Setup(x => x.FindValidatorsInAssemblies(It.IsAny<IEnumerable<Assembly>>()))
                .Returns(validatorTypes);

            var services = new ServiceCollection()
                .AddFluentValidation(configure => configure
                .UsingAssemblyScanner(assemblyScanner.Object, scan => scan
                    .Scan(Enumerable.Empty<Assembly>())
                    .WithLifeTime(serviceLifetime)
                    .WithInterfaces(mapInterfaces)
                    ));

            var validatorServices = services
                .Where(x => IsValidatorType(x.ServiceType))
                .ToList();

            Assert.All(validatorServices, x => Assert.Equal(serviceLifetime, x.Lifetime));

            foreach(var validatorType in validatorTypes)
                Assert.Contains(validatorServices, x => x.ServiceType == validatorType);
            
            if (mapInterfaces)
                foreach (var validatorType in validatorTypes)
                    Assert.Contains(validatorServices, x => x.ServiceType == GetValidatorInterface(validatorType));
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped, false)]
        [InlineData(ServiceLifetime.Singleton, false)]
        [InlineData(ServiceLifetime.Transient, false)]
        [InlineData(ServiceLifetime.Scoped, true)]
        [InlineData(ServiceLifetime.Singleton, true)]
        [InlineData(ServiceLifetime.Transient, true)]
        public void Should_Add_Validators(ServiceLifetime serviceLifetime, bool mapInterfaces)
        {
            var validatorTypes = new[] { typeof(CustomerValidator), typeof(PhoneValidator) };

            var services = new ServiceCollection()
                .AddFluentValidation(configure => configure
                .AddValidators(validatorTypes, serviceLifetime, mapInterfaces));

            var validatorServices = services
                .Where(x => IsValidatorType(x.ServiceType))
                .ToList();

            Assert.All(validatorServices, x => Assert.Equal(serviceLifetime, x.Lifetime));

            foreach (var validatorType in validatorTypes)
                Assert.Contains(validatorServices, x => x.ServiceType == validatorType);

            if (mapInterfaces)
                foreach (var validatorType in validatorTypes)
                    Assert.Contains(validatorServices, x => x.ServiceType == GetValidatorInterface(validatorType));
        }
    }
}