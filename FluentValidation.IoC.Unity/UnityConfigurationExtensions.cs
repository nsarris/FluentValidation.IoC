using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Microsoft.DependencyInjection;

namespace FluentValidation.IoC.Unity
{
    public static class UnityConfigurationExtensions
    {
        public static IUnityContainer RegisterFluentValidation(this IUnityContainer container, Action<FluentValidationConfiguration> configure)
        {
            var services = new UnityServiceCollection();
            services.AddFluentValidation(configure);

            container.RegisterInstance<IServiceProvider>(new UnityServiceProvider(container));

            container.AddServices(services);

            return container;
        }

        public static IUnityContainer RegisterLiteralService<TLiteralService>(this IUnityContainer container, TLiteralService literalService)
            where TLiteralService : ILiteralService
        {
            container.RegisterInstance(literalService);
            container.RegisterType<ILiteralService, TLiteralService>();

            return container;
        }

        public static IUnityContainer RegisterLiteralService<TLiteralService>(this IUnityContainer container, ITypeLifetimeManager lifetimeManager)
            where TLiteralService : ILiteralService
        {
            container.RegisterType<ILiteralService, TLiteralService>(lifetimeManager);

            return container;
        }
    }
}
