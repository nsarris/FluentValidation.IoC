using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation.IoC
{
    public class FluentValidationConfiguration
    {
        internal FluentValidationConfiguration(IServiceCollection services)
        {
            Services = services;
        }

        internal IServiceCollection Services { get; }

        internal ServiceDescriptor ValidatorProviderServiceDescriptor { get; private set; } = new ServiceDescriptor(typeof(DefaultValidatorProvider), typeof(DefaultValidatorProvider), ServiceLifetime.Transient);
        internal List<ValidatorServiceDescriptor> ValidatorServiceDescriptors { get; } = new List<ValidatorServiceDescriptor>();
        internal IDuplicateValidatorResolutionStrategy DuplicateResolver { get; private set; }

        public FluentValidationConfiguration AddValidatorProvider(Type validatorProviderType, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            ValidatorProviderServiceDescriptor = new ServiceDescriptor(validatorProviderType, validatorProviderType, lifetime);

            return this;
        }

        public FluentValidationConfiguration AddValidators(IEnumerable<Type> validatorTypes, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            

            foreach (var validatorType in validatorTypes)
                ValidatorServiceDescriptors.Add(new ValidatorServiceDescriptor(validatorType, lifetime, mapInterfaces));

            return this;
        }

        public FluentValidationConfiguration UsingAssemblyScanner(IAssemblyScanner assemblyScanner, Action<AssemblyScannerConfiguration> configure)
        {
            var assemblyScannerConfiguration = new AssemblyScannerConfiguration(assemblyScanner ?? new DefaultAssemblyScanner());
            configure?.Invoke(assemblyScannerConfiguration);

            foreach (var validatorType in assemblyScannerConfiguration.ValidatorTypes)
                ValidatorServiceDescriptors.Add(new ValidatorServiceDescriptor(validatorType, assemblyScannerConfiguration.LifeTime, assemblyScannerConfiguration.MapInterfaces));

            return this;
        }

        public FluentValidationConfiguration WithDuplicateResolution(IDuplicateValidatorResolutionStrategy duplicateValidatorResolutionStrategy)
        {
            DuplicateResolver = duplicateValidatorResolutionStrategy;
            return this;
        }
    }
}
