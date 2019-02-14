﻿using System;
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

        public static IUnityContainer RegisterResolver<TResolver>(this IUnityContainer container)
            where TResolver : IDependencyResolver
        {
            container.RegisterType<IDependencyResolver, TResolver>();

            RegisterIoCValidationContext(container);

            return container;
        }

        public static IUnityContainer RegisterDefaultFactory(this IUnityContainer container)
        {
            container.RegisterFactory<IValidatorFactory>(c => new DefaultValidatorFactory(new UnityServiceProvider(c)));

            return container;
        }

        
        public static IUnityContainer RegisterFactory<TValidatorFactory>(this IUnityContainer container)
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

        public static IUnityContainer RegisterAllValidatorsAsSingletons(this IUnityContainer container, bool mapInterfaces = true)
        {
            container.RegisterAllValidatorsAsSingletons(AllClassesEx.FromAssembliesInBasePath(), mapInterfaces);

            return container;
        }

        public static IUnityContainer RegisterAllValidatorsAsSingletons(this IUnityContainer container, IEnumerable<Assembly> assemblies, bool mapInterfaces = true)
        {
            container.RegisterAllValidatorsAsSingletons(
                assemblies
                    .Where(x =>
                    x != typeof(AbstractValidator<>).Assembly)
                        .SelectMany(x => x.GetTypes()), 
                mapInterfaces);

            return container;
        }

        public static IUnityContainer RegisterAllValidatorsAsSingletons(this IUnityContainer container, IEnumerable<Type> types, bool mapInterfaces = true)
        {
            var validatorTypes = types
                    .Where(x =>
                        x.Assembly != typeof(AbstractValidator<>).Assembly
                        && !x.IsGenericType
                        && IsSubclassOfGeneric(x, typeof(AbstractValidator<>)))
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
                typesToRegister,
                t => new[] { t }.Concat(
                    mapInterfaces ?
                        WithMappings.FromAllInterfaces(t)
                        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)) :
                        Enumerable.Empty<Type>()),
                WithName.Default,
                _ => new SingletonLifetimeManager());

            return container;
        }


        private static bool IsSubclassOfGeneric(Type type, Type openGenericType)
        {
            return IsOrSubclassOfGeneric(type.BaseType, openGenericType);
        }

        private static bool IsOrSubclassOfGeneric(Type type, Type genericType)
        {
            var openGenericType = genericType.IsGenericType ? genericType.GetGenericTypeDefinition() : genericType;

            while (type != typeof(object))
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == openGenericType)
                    return true;

                type = type.BaseType;
            }

            return false;
        }
    }
}
