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
        private static void RegisterIoCValidationContext(this IUnityContainer container)
        {
            container.RegisterType<IoCValidationContext>(new InjectionConstructor(new[] { typeof(IDependencyResolver), typeof(IValidatorFactory) }));
        }

        public static void RegisterResolverAndFactory<TResolver>(this IUnityContainer container)
            where TResolver : IDependencyResolver, IValidatorFactory
        {
            container.RegisterType<IDependencyResolver, TResolver>();
            container.RegisterType<IValidatorFactory, TResolver>();

            RegisterIoCValidationContext(container);
        }

        public static void RegisterResolver<TResolver>(this IUnityContainer container)
            where TResolver : IDependencyResolver
        {
            container.RegisterType<IDependencyResolver, TResolver>();

            RegisterIoCValidationContext(container);
        }

        public static void RegisterFactory<TValidatorFactory>(this IUnityContainer container)
            where TValidatorFactory : IValidatorFactory
        {
            container.RegisterType<IValidatorFactory, TValidatorFactory>();

            RegisterIoCValidationContext(container);
        }

        public static void RegisterLiteralService<TLiteralService>(this IUnityContainer container, TLiteralService literalService)
            where TLiteralService : ILiteralService
        {
            container.RegisterInstance(literalService);
            container.RegisterType<ILiteralService, TLiteralService>();
        }

        public static void RegisterLiteralService<TLiteralService>(this IUnityContainer container, LifetimeManager lifetimeManager)
            where TLiteralService : ILiteralService
        {
            container.RegisterType<ILiteralService, TLiteralService>(lifetimeManager);
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
            var validatorTypes = types
                    .Where(x =>
                        x.BaseType != null
                        && x.BaseType.IsGenericType
                        && x.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                     .GroupBy(x => x.BaseType.GetGenericArguments().First(), x => new
                     {
                         Type = x, IsDefault = x.CustomAttributes.Any(a => a.AttributeType == typeof(DefaultValidatorAttribute))
                     })
                     .Select(x => new
                     {
                         ElementType = x.Key,
                         DefaultValidators = x.Count() == 1 ? new[] { x.First() }.ToList() : x.Where(a => a.IsDefault).ToList(),
                         Validators = x.ToList()
                     })
                     .ToList();

            var multipleDefaultValidators = validatorTypes.Where(x => x.DefaultValidators.Count > 1).ToList();

            if (multipleDefaultValidators.Any())
                throw new InvalidOperationException("The DefaultValidatorAttribute can only be used once per IValidator<T> implementation. The following violations where found: "
                    + Environment.NewLine + 
                    string.Join(Environment.NewLine,
                        multipleDefaultValidators
                            .Select(v => $"Validated type: {v.ElementType} , ValidatorTypes : {string.Join(",", v.Validators.Select(vv => vv.Type.Name))}")));

            var duplicateValidatorTypes = validatorTypes.Where(x => x.Validators.Count > 1 && !x.DefaultValidators.Any()).ToList();

            if (duplicateValidatorTypes.Any())
                throw new InvalidOperationException("Duplicate validators found for the same validater type. Please use the DefaultValidationAttribute to select the default validator. The following violations where found: "
                    + Environment.NewLine +
                    string.Join(Environment.NewLine,
                        duplicateValidatorTypes
                            .Select(v => $"Validated type: {v.ElementType} , ValidatorTypes : {string.Join(",", v.Validators.Select(vv => vv.Type.Name))}")));

            var typesToRegister = validatorTypes.Select(x => x.Validators.OrderByDescending(v => v.IsDefault).Select(v => v.Type).First());

            container.RegisterTypes(
                typesToRegister
                    .Where(x => 
                        x.BaseType.IsGenericType
                        && x.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                     ,
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
