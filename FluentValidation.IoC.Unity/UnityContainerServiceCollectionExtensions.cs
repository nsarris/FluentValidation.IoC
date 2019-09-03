using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Lifetime;

namespace FluentValidation.IoC.Unity
{
    internal static class UnityContainerServiceCollectionExtensions
    {
        internal static IUnityContainer ConfigureServices(this IUnityContainer container, Action<IServiceCollection> configure)
        {
            var services = new UnityServiceCollection();
            configure?.Invoke(services);

            container.ConfigureServices(services);

            return container;
        }

        internal static IUnityContainer ConfigureServices(this IUnityContainer container, IServiceCollection services)
        {
            foreach (var group in services.GroupBy(serviceDescriptor => serviceDescriptor.ServiceType)
                                          .Select(group => group.ToArray()))
            {
                var last = group.Last();

                // Register named types
                foreach (var descriptor in group.Where(x => x != last))
                {
                    container.Register(descriptor, Guid.NewGuid().ToString());
                }

                // Register default types
                container.Register(last, null);
            }

            return container;
        }

        private static void Register(this IUnityContainer container,
            ServiceDescriptor serviceDescriptor, string qualifier)
        {
            if (serviceDescriptor.ImplementationType != null)
            {
                container.RegisterType(serviceDescriptor.ServiceType,
                                       serviceDescriptor.ImplementationType,
                                       qualifier,
                                       (ITypeLifetimeManager)serviceDescriptor.GetLifetime());
            }
            else if (serviceDescriptor.ImplementationFactory != null)
            {
                container.RegisterFactory(serviceDescriptor.ServiceType,
                                       qualifier,
                                        scope =>
                                        {
                                            var serviceProvider = serviceDescriptor.Lifetime == ServiceLifetime.Scoped
                                                ? scope.Resolve<IServiceProvider>()
                                                : container.Resolve<IServiceProvider>();
                                            var instance = serviceDescriptor.ImplementationFactory(serviceProvider);
                                            return instance;
                                        },
                                       (IFactoryLifetimeManager)serviceDescriptor.GetLifetime());
            }
            else if (serviceDescriptor.ImplementationInstance != null)
            {
                container.RegisterInstance(serviceDescriptor.ServiceType,
                                           qualifier,
                                           serviceDescriptor.ImplementationInstance,
                                           (IInstanceLifetimeManager)serviceDescriptor.GetLifetime(true)
                                           );
            }
            else
            {
                throw new InvalidOperationException("Unsupported registration type");
            }
        }


        private static LifetimeManager GetLifetime(this ServiceDescriptor serviceDescriptor, bool isImplementationInstance = false)
        {
            switch (serviceDescriptor.Lifetime)
            {
                case ServiceLifetime.Scoped:
                    return isImplementationInstance ? (LifetimeManager)new ContainerControlledLifetimeManager() : new HierarchicalLifetimeManager();
                case ServiceLifetime.Singleton:
                    return new SingletonLifetimeManager();
                case ServiceLifetime.Transient:
                    return new ContainerControlledTransientManager();
                default:
                    throw new NotImplementedException(
                        $"Unsupported lifetime manager type '{serviceDescriptor.Lifetime}'");
            }
        }
    }
}