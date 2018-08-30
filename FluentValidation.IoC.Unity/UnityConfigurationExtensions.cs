using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace FluentValidation.IoC.Unity
{
    public static class UnityConfigurationExtensions
    {
        public static void RegisterResolverAndFactory<TResolver>(this IUnityContainer container)
            where TResolver : IDependencyResolver, IValidatorFactory
        {
            container.RegisterType<IDependencyResolver, TResolver>();
            container.RegisterType<IValidatorFactory, TResolver>();
        }

        public static void RegisterResolver<TResolver>(this IUnityContainer container)
            where TResolver : IDependencyResolver
        {
            container.RegisterType<IDependencyResolver, TResolver>();
        }

        public static void RegisterFactory<TValidatorFactory>(this IUnityContainer container)
            where TValidatorFactory : IValidatorFactory
        {
            container.RegisterType<IValidatorFactory, TValidatorFactory>();
        }

        public static void RegisterAllValidatorsAsSingletons(this IUnityContainer container, bool mapInterfaces = true)
        {
            container.RegisterAllValidatorsAsSingletons(AllClasses.FromAssembliesInBasePath(), mapInterfaces);
        }

        public static void RegisterAllValidatorsAsSingletons(this IUnityContainer container, IEnumerable<Assembly> assemblies, bool mapInterfaces = true)
        {
            container.RegisterAllValidatorsAsSingletons(
                assemblies
                    .Where(x =>
                    x != typeof(AbstractValidator<>).Assembly)
                        .SelectMany(x => x.GetTypes()), 
                mapInterfaces);
        }

        public static void RegisterAllValidatorsAsSingletons(this IUnityContainer container, IEnumerable<Type> types, bool mapInterfaces = true)
        {
            container.RegisterTypes
                (types
                    .Where(x => 
                        x.BaseType.IsGenericType
                        && x.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>)),
                t => new[] { t }.Concat(
                    mapInterfaces ?
                        WithMappings.FromAllInterfaces(t)
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)) :
                        Enumerable.Empty<Type>()),
                WithName.Default,
                _ => new SingletonLifetimeManager());
        }
    }
}
