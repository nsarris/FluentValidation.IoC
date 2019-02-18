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
        public static IServiceCollection AddFluentValidationIocExtensions(this IServiceCollection services, IEnumerable<Assembly> assemblies = null)
        {
            return services
                .AddValidationContextProvider()
                .AddDefaultValidatorFactory()
                .AddValidators(assemblies);
        }

        public static IServiceCollection AddValidationContextProvider(this IServiceCollection services)
        {
            return services.AddTransient(sp => new ValidationContextProvider(sp.GetRequiredService<IServiceProvider>()));
        }

        
        public static IServiceCollection AddDefaultValidatorFactory(this IServiceCollection services)
        {
            services.AddTransient<IValidatorFactory>(sp => new DefaultValidatorFactory(sp));

            return services;
        }


        public static IServiceCollection AddValidatorFactory<TValidatorFactory>(this IServiceCollection services)
            where TValidatorFactory : class, IValidatorFactory
        {
            services.AddTransient<IValidatorFactory, TValidatorFactory>();

            return services;
        }

        public static IServiceCollection AddLiteralService<TLiteralService>(this IServiceCollection services, TLiteralService literalService)
            where TLiteralService : class, ILiteralService
        {
            services.AddSingleton(literalService);
            services.AddSingleton<ILiteralService, TLiteralService>();

            return services;
        }

        public static IServiceCollection AddLiteralService<TLiteralService>(this IServiceCollection services, ServiceLifetime lifetime)
            where TLiteralService : ILiteralService
        {
            services.Add(new ServiceDescriptor(typeof(ILiteralService), typeof(TLiteralService), lifetime));

            return services;
        }


        public static IServiceCollection AddValidators(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            services.AddValidators((IEnumerable<Type>)null, lifetime, mapInterfaces);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Transient, bool mapInterfaces = true)
        {
            services.AddValidators(
                assemblies != null ? ReflectionHelper.AutoDiscoverValidatorTypes(assemblies) : null,
                lifetime,
                mapInterfaces);

            return services;
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

            services.Scan(x =>
                x.AddTypes(validatorTypes)
                .AddClasses(c => c
                    .Where(cc => cc.Assembly != typeof(IValidator).Assembly)
                    .AssignableTo(typeof(IValidator<>)))
                .As(t => t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
                .WithLifetime(lifetime));

            return services;
        }
    }
}
