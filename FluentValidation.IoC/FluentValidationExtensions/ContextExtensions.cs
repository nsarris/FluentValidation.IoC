using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidation.IoC
{
    public static class ContextExtensions
    {
        #region ValidationContext

        internal static void SetServiceProvider(this ValidationContext context, IServiceProvider serviceProvider)
        {
            context.RootContextData[Constants.ServiceProviderKeyLiteral] = serviceProvider;
        }

        public static IServiceProvider GetServiceProvider(this ValidationContext context)
        {
            var serviceProvider = (IServiceProvider)context.RootContextData[Constants.ServiceProviderKeyLiteral]
                    ?? ServiceLocator.GetServiceProvider();

            if (serviceProvider == null)
                throw new InvalidOperationException("Could not get a service provider for validation. Either use an IoCValidationContext or register a global service provider in ServiceLocator.");

            return serviceProvider;
        }

        public static IValidatorFactory GetValidatorFactory(this ValidationContext context)
        {
            var factory = context.GetServiceProvider().GetValidatorFactory()
                ?? ServiceLocator.GetValidatorFactory();

            if (factory == null)
                throw new InvalidOperationException("Could not get a validator factory. Either use an IoCValidationContext or register a global validator factory in ServiceLocator.");

            return factory;
        }

        public static ILiteralService GetLiteralService(this ValidationContext context)
        {
            return context.GetServiceProvider().GetRequiredService<ILiteralService>();
        }

        public static ValidationResult ValidateUsing<TValidator>(this ValidationContext context)
            where TValidator : IValidator
            => context.GetValidatorFactory().GetSpecificValidator<TValidator>().Validate(context);

        public static ValidationResult ValidateUsing<T, TValidator>(this ValidationContext<T> context)
            where TValidator : IValidator<T>
            => context.GetValidatorFactory().GetSpecificValidator<TValidator>().Validate(context);

        public static ValidationResult Validate<T>(this ValidationContext<T> context)
            => context.GetValidatorFactory().GetValidator<T>().Validate(context);


        #endregion

        #region Custom and PropertyValidator context

        public static IServiceProvider GetServiceProvider(this CustomContext context)
        {
            return GetServiceProvider(context.ParentContext);
        }
        public static IServiceProvider GetServiceProvider(this PropertyValidatorContext context)
        {
            return GetServiceProvider(context.ParentContext);
        }

        public static IValidatorFactory GetValidatorFactory(this CustomContext context)
        {
            return GetValidatorFactory(context.ParentContext);
        }

        public static IValidatorFactory GetValidatorFactory(this PropertyValidatorContext context)
        {
            return GetValidatorFactory(context.ParentContext);
        }

        public static TValidator ResolveValidator<TChild, TValidator>(this CustomContext context)
            where TValidator : IValidator<TChild>
        {
            return GetValidatorFactory(context).GetSpecificValidator<TValidator>();
        }

        public static TValidator ResolveValidator<TChild, TValidator>(this PropertyValidatorContext context)
            where TValidator : IValidator<TChild>
        {
            return GetValidatorFactory(context).GetSpecificValidator<TValidator>();
        }

        public static IValidator<TChild> ResolveValidator<TChild>(this PropertyValidatorContext context, Type validatorType)
        {
            return (IValidator<TChild>)GetValidatorFactory(context).GetSpecificValidator(validatorType);
        }

        public static IValidator<TChild> ResolveValidator<TChild>(this CustomContext context, Type validatorType)
        {
            return (IValidator<TChild>)GetValidatorFactory(context).GetSpecificValidator(validatorType);
        }

        public static IValidator<TChild> ResolveValidator<TChild>(this CustomContext context)
        {
            return GetValidatorFactory(context).GetValidator<TChild>();
        }

        public static IValidator<TChild> ResolveValidator<TChild>(this PropertyValidatorContext context)
        {
            return GetValidatorFactory(context).GetValidator<TChild>();
        }

        public static TDependency ResolveDependency<TDependency>(this CustomContext context)
        {
            return GetServiceProvider(context).GetRequiredService<TDependency>();
        }

        public static TDependency ResolveDependency<TDependency>(this PropertyValidatorContext context)
        {
            return GetServiceProvider(context).GetRequiredService<TDependency>();
        }

        public static CustomContext Append(this CustomContext validationContext, ValidationResult result)
        {
            foreach (var failure in result.Errors)
                validationContext.AddFailure(failure);
            return validationContext;
        }

        #endregion
    }
}
