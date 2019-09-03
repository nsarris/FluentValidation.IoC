using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FluentValidation.IoC
{
    public static class FluentValidationConfigurationExtensions
    {
        public static FluentValidationConfiguration AddValidatorProvider(this FluentValidationConfiguration configuration, Type validatorProviderType)
            => configuration.AddValidatorProvider(validatorProviderType, ServiceLifetime.Transient);

        public static FluentValidationConfiguration AddValidatorProvider<T>(this FluentValidationConfiguration configuration, ServiceLifetime lifetime)
            where T : IValidatorProvider
            => configuration.AddValidatorProvider(typeof(T), lifetime);

        public static FluentValidationConfiguration AddValidatorProvider<T>(this FluentValidationConfiguration configuration)
            where T : IValidatorProvider
            => configuration.AddValidatorProvider<T>(ServiceLifetime.Transient);

        public static FluentValidationConfiguration AddValidator(this FluentValidationConfiguration configuration, Type validatorType, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
            => configuration.AddValidators(new[] { validatorType }, lifetime, mapInterfaces);

        public static FluentValidationConfiguration UsingAssemblyScanner(this FluentValidationConfiguration configuration, Action<AssemblyScannerConfiguration> configure)
            => configuration.UsingAssemblyScanner(null, configure);

        public static FluentValidationConfiguration WithDuplicateResolution(this FluentValidationConfiguration configuration, Func<IEnumerable<ServiceDescriptor>, ServiceDescriptor> resolve)
            => configuration.WithDuplicateResolution(new FuncWrapperDuplicateResolutionStrategy(resolve));

        public static FluentValidationConfiguration WithDuplicateResolutionByAttribute(this FluentValidationConfiguration configuration, Type attributeType)
            => configuration.WithDuplicateResolution(new AttributeDuplicateResolutionStrategy(attributeType));

        public static FluentValidationConfiguration WithDuplicateResolutionByAttribute<T>(this FluentValidationConfiguration configuration)
            where T : Attribute
            => configuration.WithDuplicateResolution(new AttributeDuplicateResolutionStrategy(typeof(T)));
    }
}
