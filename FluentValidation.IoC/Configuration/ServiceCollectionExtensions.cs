using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentValidation.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Action<FluentValidationConfiguration> configure)
        {
            var configuration = new FluentValidationConfiguration(services);
            configure?.Invoke(configuration);
            return AddFluentValidation(configuration);
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            return AddFluentValidation(new FluentValidationConfiguration(services));
        }

        private static IServiceCollection AddFluentValidation(FluentValidationConfiguration configuration)
        {
            configuration.Services.AddTransient<IValidationContextProvider, ValidationContextProvider>();
            configuration.Services.AddTransient(typeof(IValidationContextProvider<>), typeof(ValidationContextProvider<>));
            configuration.Services.AddTransient(typeof(IValidationContextProvider<,>), typeof(ValidationContextProvider<,>));

#pragma warning disable CS0618 // Type or member is obsolete
            configuration.Services.AddScoped<IValidatorFactory, ServiceProviderValidatorFactory>();
#pragma warning restore CS0618 // Type or member is obsolete

            var interfaceMaps = configuration.ValidatorServiceDescriptors
                .Select(x => new
                {
                    x.ValidatorType,
                    x.Lifetime,
                    Interfaces = x.MapInterfaces ? x.ValidatorType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)) : Enumerable.Empty<Type>()
                })
                .ToList();

            var interfaceServiceDescriptors = interfaceMaps
                .SelectMany(x =>
                    x.Interfaces.Select(y => new ServiceDescriptor(y, x.ValidatorType, x.Lifetime)))
                .ToList();

            if (configuration.DuplicateResolver != null)
            {
                var duplicates = interfaceServiceDescriptors
                    .GroupBy(x => x.ServiceType)
                    .Where(x => x.Count() > 1)
                    .SelectMany(x => x)
                    .ToList();

                interfaceServiceDescriptors = interfaceServiceDescriptors
                    .Except(duplicates)
                    .Concat(new[] { configuration.DuplicateResolver.Resolve(duplicates) })
                    .ToList();
            }

            foreach (var implementationDescriptor in interfaceMaps
                   .Select(x => new ServiceDescriptor(x.ValidatorType, x.ValidatorType, x.Lifetime)))
                configuration.Services.Add(implementationDescriptor);

            foreach (var implementationDescriptor in interfaceServiceDescriptors
                   .Select(x => new ServiceDescriptor(x.ServiceType, sp => sp.GetRequiredService(x.ImplementationType), x.Lifetime)))
                configuration.Services.Add(implementationDescriptor);

            return configuration.Services;
        }
    }
}
