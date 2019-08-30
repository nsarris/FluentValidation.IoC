using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FluentValidation.IoC
{
    public interface IFluentValidationConfiguration
    {
        IFluentValidationConfiguration AddValidatorProvider(Type validatorProviderType, ServiceLifetime lifetime);
    }

    public class ValidatorDescriptor
    {
        public ValidatorDescriptor(Type validatorType, Type interfaceType)
        {
            ValidatorType = validatorType;
            InterfaceType = interfaceType;
        }

        public Type ValidatorType { get; }
        public Type InterfaceType { get; }
    }

    public interface IAssemblyScanner
    {
        IEnumerable<ValidatorDescriptor> FindValidatorsInAssemblies(IEnumerable<Assembly> assemblies);
    }

    public class DefaultAssemblyScanner : IAssemblyScanner
    {
        public IEnumerable<ValidatorDescriptor> FindValidatorsInAssemblies(IEnumerable<Assembly> assemblies)
        {
            return AssemblyScanner
                .FindValidatorsInAssemblies(assemblies)
                .Select(x => new ValidatorDescriptor(x.ValidatorType, x.InterfaceType))
                .ToList();
        }
    }

    public static class AssemblyScannerExtensions
    {
        public static IEnumerable<ValidatorDescriptor> FindValidatorsInAssembly(this IAssemblyScanner assemblyScanner, Assembly assembly)
            => assemblyScanner.FindValidatorsInAssemblies(new[] { assembly });

        public static IEnumerable<ValidatorDescriptor> FindValidatorsInAssemblyContaining<T>(this IAssemblyScanner assemblyScanner)
            => assemblyScanner.FindValidatorsInAssemblyContaining(typeof(T));

        public static IEnumerable<ValidatorDescriptor> FindValidatorsInAssemblyContaining(this IAssemblyScanner assemblyScanner, Type type)
            => assemblyScanner.FindValidatorsInAssembly(type.Assembly);
    }

    internal class FluentValidationConfiguration : IFluentValidationConfiguration
    {
        public FluentValidationConfiguration(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public Type ValidatorProviderType { get; private set; }
        public ServiceLifetime ValidatorProviderLifeTime { get; private set; } = ServiceLifetime.Transient;
        public List<Type> ValidatorTypes { get; private set; } = new List<Type>();

        private IAssemblyScanner assemblyScanner = new DefaultAssemblyScanner();

        public IFluentValidationConfiguration AddValidatorProvider(Type validatorProviderType, ServiceLifetime lifetime)
        {
            ValidatorProviderType = validatorProviderType;
            ValidatorProviderLifeTime = lifetime;

            return this;
        }

        public IFluentValidationConfiguration UseAssemblyScanner(IAssemblyScanner assemblyScanner)
        {
            this.assemblyScanner = assemblyScanner;
            return this;
        }

        public IFluentValidationConfiguration ScanValidators(IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            return AddValidators(assemblyScanner.FindValidatorsInAssemblies(assemblies), lifetime, mapInterfaces);
        }

        public IFluentValidationConfiguration ScanValidators(Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            return ScanValidators(new[] { assembly }, lifetime, mapInterfaces);
        }

        public IFluentValidationConfiguration AddValidators(IEnumerable<ValidatorDescriptor> validatorDescriptors, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            foreach (var validatorDescriptor in validatorDescriptors)
                Services.Add(new ServiceDescriptor(validatorDescriptor.InterfaceType, validatorDescriptor.ValidatorType, lifetime));
            
            return this;
        }

        public IFluentValidationConfiguration AddValidators(IEnumerable<Type> validatorTypes, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            foreach (var validatorType in validatorTypes)
            {
                Services.Add(new ServiceDescriptor(validatorType, lifetime));
                if (mapInterfaces)
                {
                    foreach (var validatorInterface in validatorType.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                    {
                        Services.Add(new ServiceDescriptor(validatorInterface, validatorType, lifetime));
                    }
                }
            }

            return this;
        }
    }

    public static class FluentValidationConfigurationExtensions
    {
        public static IFluentValidationConfiguration AddValidatorProvider(this IFluentValidationConfiguration configuration, Type validatorProviderType)
        {
            return configuration.AddValidatorProvider(validatorProviderType, ServiceLifetime.Transient);
        }

        public static IFluentValidationConfiguration AddValidatorProvider<T>(this IFluentValidationConfiguration configuration, ServiceLifetime lifetime)
            where T : IValidatorProvider
        {
            return configuration.AddValidatorProvider(typeof(T), lifetime);
        }

        public static IFluentValidationConfiguration AddValidatorProvider<T>(this IFluentValidationConfiguration configuration)
            where T : IValidatorProvider
        {
            return configuration.AddValidatorProvider<T>(ServiceLifetime.Transient);
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidationIocExtensions(this IServiceCollection services, Action<IFluentValidationConfiguration> configure)
        {
            var configuration = new FluentValidationConfiguration(services);
            configure?.Invoke(configuration);

            services.Add(new ServiceDescriptor(typeof(IValidatorProvider), configuration.ValidatorProviderType ?? typeof(DefaultValidatorProvider), configuration.ValidatorProviderLifeTime));
            services.Add(new ServiceDescriptor(typeof(IValidatorProvider), sp => sp.GetRequiredService<IValidatorProvider>(), configuration.ValidatorProviderLifeTime));

            return services;
        }

        public static IServiceCollection AddFluentValidationIocExtensions(this IServiceCollection services, IEnumerable<Assembly> assemblies = null)
        {
            return services
                .AddValidationContextProvider()
                .AddDefaultValidatorProvider()
                .AddValidators(assemblies);
        }

        public static IServiceCollection AddValidationContextProvider(this IServiceCollection services)
        {
            return services.AddTransient(sp => new ValidationContextProvider(sp.GetRequiredService<IServiceProvider>()));
        }

        public static IServiceCollection AddDefaultValidatorProvider(this IServiceCollection services)
        {
            return services
                .AddTransient<IValidatorProvider, DefaultValidatorProvider>()
                .AddTransient<IValidatorFactory, DefaultValidatorProvider>();
        }

        public static IServiceCollection AddValidatorProvider<TValidatorProvider>(this IServiceCollection services)
            where TValidatorProvider : class, IValidatorProvider
        {
            return services
                .AddTransient<IValidatorProvider, TValidatorProvider>()
                .AddTransient<IValidatorFactory, TValidatorProvider>();
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            return services.AddValidators((IEnumerable<Type>)null, lifetime, mapInterfaces);
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            return services.AddValidators(
                assemblies != null ? ReflectionHelper.AutoDiscoverValidatorTypes(assemblies) : null,
                lifetime,
                mapInterfaces);
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, IEnumerable<Type> validatorTypes, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            services.Scan(x =>
                x.FromApplicationDependencies()
                .AddClasses(c => c
                    .Where(cc => cc.Assembly != typeof(IValidator).Assembly)
                    .AssignableTo(typeof(IValidator<>)))
                .AsSelf()
                .WithLifetime(lifetime));

            validatorTypes = validatorTypes ?? ReflectionHelper.AutoDiscoverValidatorTypes(AppDomain.CurrentDomain.GetAssemblies());

            if (mapInterfaces)
            {
                services.Scan(x =>
                    x.AddTypes(validatorTypes)
                    .AddClasses(c => c
                        .Where(cc => cc.Assembly != typeof(IValidator).Assembly)
                        .AssignableTo(typeof(IValidator<>)))
                    .As(t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                    .WithLifetime(lifetime));
            }

            return services;
        }
    }
}
