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

namespace FluentValidation.IoC.Unity
{
    public static class UnityContainerExtensions
    {
        public static IUnityContainer RegisterFluentValidation(this IUnityContainer container, Action<FluentValidationConfiguration> configure)
        {
            if (!container.IsRegistered<IServiceProvider>())
                container.RegisterFactory<IServiceProvider>(scope => new UnityServiceProvider(scope), new ContainerControlledLifetimeManager());

            container.ConfigureServices(services => services.AddFluentValidation(configure));

            return container;
        }
    }
}
