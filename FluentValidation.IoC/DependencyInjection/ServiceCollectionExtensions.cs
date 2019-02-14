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
        private static void RegisterIoCValidationContext(this IServiceCollection services)
        {
            services.AddTransient(sp => new IoCValidationContext(sp.GetRequiredService<IDependencyResolver>()));
        }

        public static IServiceCollection AddValidationIocExtensions(this IServiceCollection services, IEnumerable<Assembly> assemblies = null)
            => AddValidationIocExtensions<DefaultDependencyResolver>(services, assemblies);

        public static IServiceCollection AddValidationIocExtensions<TResolver>(this IServiceCollection services, IEnumerable<Assembly> assemblies = null)
            where TResolver : class, IDependencyResolver
        {
            return services
                .AddValidationResolver<TResolver>()
                .AddDefaultValidatorFactory()
                .AddValidators(assemblies);
        }

        public static IServiceCollection AddDefaultResolver(this IServiceCollection services)
        {
            return services.AddValidationResolver<DefaultDependencyResolver>();
        }

        public static IServiceCollection AddValidationResolver<TResolver>(this IServiceCollection services)
            where TResolver : class, IDependencyResolver
        {
            if (typeof(TResolver) == typeof(DefaultDependencyResolver))
                services.AddTransient<IDependencyResolver>(sp => new DefaultDependencyResolver(sp));
            else
                services.AddTransient<IDependencyResolver, TResolver>();

            RegisterIoCValidationContext(services);

            return services;
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


        public static IServiceCollection AddValidators(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton, bool mapInterfaces = true)
        {
            services.AddValidators((IEnumerable<Type>)null, lifetime, mapInterfaces);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime lifetime = ServiceLifetime.Singleton, bool mapInterfaces = true)
        {
            services.AddValidators(
                assemblies != null ? ReflectionHelper.AutoDiscoverValidatorTypes(assemblies) : null,
                lifetime,
                mapInterfaces);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, IEnumerable<Type> validatorTypes, ServiceLifetime lifetime = ServiceLifetime.Singleton, bool mapInterfaces = true)
        {
            services.Scan(x =>
                (validatorTypes == null ? x.FromApplicationDependencies() : x.AddTypes(validatorTypes))
                .AddClasses(c => c
                    .Where(cc => cc.Assembly != typeof(IValidator).Assembly)
                    .AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .AsSelf()
                .WithLifetime(lifetime));

            return services;
        }
    }
}
