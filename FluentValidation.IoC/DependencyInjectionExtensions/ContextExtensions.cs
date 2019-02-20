using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.Internal;

namespace FluentValidation.IoC
{
    public static class ContextExtensions
    {
        #region ValidationContext

        internal static bool TryGetValidationContext(this IValidationContext sourceContext, out ValidationContext validationContext)
        {
            validationContext = null;
            if (sourceContext is ValidationContext context)
                validationContext = context;
            else if (sourceContext is PropertyValidatorContext propertyValidatorContext)
                validationContext = propertyValidatorContext.ParentContext;
            else if (sourceContext is MessageBuilderContext messageBuilderContext)
                validationContext = messageBuilderContext.ParentContext;
            else if (sourceContext is CustomContext customContext)
                validationContext = customContext.ParentContext;

            return validationContext != null;
        }

        internal static ValidationContext GetValidationContext(this IValidationContext sourceContext)
        {
            if (TryGetValidationContext(sourceContext, out var validationContext))
                return validationContext;

            throw new NotSupportedException($"ValidationContext of type {sourceContext.GetType()} not supported");
        }

        public static IServiceProvider GetServiceProvider(this IValidationContext context)
        {
            var serviceProvider =
                (context.GetValidationContext().RootContextData.TryGetValue(Constants.ServiceProviderKeyLiteral, out var serviceProviderObject)
                    ? serviceProviderObject as IServiceProvider : null)
                        ?? ServiceLocator.GetServiceProvider();

            if (serviceProvider == null)
                throw new InvalidOperationException("Could not get a service provider for validation. Either use a ValidationContextProvider or register a global service provider in ServiceLocator.");

            return serviceProvider;
        }

        internal static void SetServiceProvider(this IValidationContext context, IServiceProvider serviceProvider)
        {
            context.GetValidationContext().RootContextData[Constants.ServiceProviderKeyLiteral] = serviceProvider;
        }

        public static IValidatorFactory GetValidatorFactory(this IValidationContext context)
        {
            var factory = context.GetServiceProvider().GetValidatorFactory()
                ?? ServiceLocator.GetValidatorFactory();

            if (factory == null)
                throw new InvalidOperationException("Could not get a validator factory. Either use a ValidationContextProvider or register a global validator factory in ServiceLocator.");

            return factory;
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
