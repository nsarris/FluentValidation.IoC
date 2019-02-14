using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace FluentValidation.IoC.Unity
{
    public static class UnityConfigurationExtensions
    {
        public static IUnityContainer RegisterValidationContextProvider(this IUnityContainer container)
        {
            return container.RegisterType<ValidationContextProvider>(new InjectionConstructor(new[] { typeof(IServiceProvider) }));
        }

        public static IUnityContainer RegisterAsServiceProvider(this IUnityContainer container)
        {
            return 
                container.RegisterFactory<IServiceProvider>(c => new UnityServiceProvider(c));
        }

        public static IUnityContainer RegisterDefaultValidatorFactory(this IUnityContainer container)
        {
            container.RegisterFactory<IValidatorFactory>(c => new DefaultValidatorFactory(new UnityServiceProvider(c)));

            return container;
        }

        
        public static IUnityContainer RegisterValidatorFactory<TValidatorFactory>(this IUnityContainer container)
            where TValidatorFactory : IValidatorFactory
        {
            container.RegisterType<IValidatorFactory, TValidatorFactory>();

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

        public static IUnityContainer RegisterAllValidators(this IUnityContainer container, LifetimeManager lifetimeManager = null, bool mapInterfaces = true)
        {
            container.RegisterAllValidators(
                ReflectionHelper.AutoDiscoverValidatorTypes(
                    AllClassesEx.FromAssembliesInBasePath()), 
                lifetimeManager, 
                mapInterfaces);

            return container;
        }

        public static IUnityContainer RegisterAllValidators(this IUnityContainer container, IEnumerable<Assembly> assemblies, LifetimeManager lifetimeManager = null, bool mapInterfaces = true)
        {
            container.RegisterAllValidators(
                ReflectionHelper.AutoDiscoverValidatorTypes(assemblies), 
                lifetimeManager,
                mapInterfaces);

            return container;
        }

        public static IUnityContainer RegisterAllValidators(this IUnityContainer container, IEnumerable<Type> validatorTypes, LifetimeManager lifetimeManager = null, bool mapInterfaces = true)
        {
            container.RegisterTypes(
                validatorTypes,
                t => new[] { t }.Concat(
                    mapInterfaces ?
                        WithMappings.FromAllInterfaces(t)
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)) :
                        Enumerable.Empty<Type>()),
                WithName.Default,
                _ => lifetimeManager ?? new SingletonLifetimeManager());

            return container;
        }
    }
}
