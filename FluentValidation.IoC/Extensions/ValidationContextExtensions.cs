using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.Internal;
using System.Threading.Tasks;
using System.Threading;

namespace FluentValidation.IoC
{
    public static class ValidationContextExtensions
    {
        #region Internal

        //internal static bool TryGetValidationContext(this IValidationContext sourceContext, out ValidationContext validationContext)
        //{
        //    validationContext = null;
        //    if (sourceContext is ValidationContext context)
        //        validationContext = context;
        //    else if (sourceContext is PropertyValidatorContext propertyValidatorContext)
        //        validationContext = propertyValidatorContext.ParentContext;
        //    else if (sourceContext is MessageBuilderContext messageBuilderContext)
        //        validationContext = messageBuilderContext.ParentContext;
        //    else if (sourceContext is CustomContext customContext)
        //        validationContext = customContext.ParentContext;

        //    return validationContext != null;
        //}

        //internal static ValidationContext GetValidationContext(this IValidationContext sourceContext)
        //{
        //    if (TryGetValidationContext(sourceContext, out var validationContext))
        //        return validationContext;

        //    throw new NotSupportedException($"ValidationContext of type {sourceContext.GetType()} not supported");
        //}

        //public static IServiceProvider GetServiceProvider(this IValidationContext context)
        //{
        //    var serviceProvider =
        //        (context.GetValidationContext().RootContextData.TryGetValue(Constants.ServiceProviderKeyLiteral, out var serviceProviderObject)
        //            ? serviceProviderObject as IServiceProvider : null);

        //    if (serviceProvider == null)
        //        throw new InvalidOperationException("Could not get a service provider for validation. Either use a ValidationContextProvider or use SetProvider on the ValidationContext.");

        //    return serviceProvider;
        //}

        //internal static void SetServiceProvider(this IValidationContext context, IServiceProvider serviceProvider)
        //{
        //    context.GetValidationContext().RootContextData[Constants.ServiceProviderKeyLiteral] = serviceProvider;
        //}

        internal static T WithServiceProvider<T>(this T context, IServiceProvider serviceProvider)
            where T : ValidationContext
        {
            context.SetServiceProvider(serviceProvider);
            return context;
        }

        #endregion

        #region ValidationContext

        public static IValidatorProvider GetValidatorProvider(this IValidationContext context)
            => context.GetServiceProvider().GetValidatorProvider();

        public static ValidationResult Validate<T>(this ValidationContext<T> context)
            => context.GetValidatorProvider().GetValidator<T>().Validate(context);

        public static Task<ValidationResult> ValidateAsync<T>(this ValidationContext<T> context, CancellationToken cancellation = default)
            => context.GetValidatorProvider().GetValidator<T>().ValidateAsync(context, cancellation);

        public static ValidationResult ValidateUsing(this ValidationContext context, Type validatorType)
            => context.GetValidatorProvider().GetSpecificValidator(validatorType).Validate(context);

        public static ValidationResult ValidateUsing<T>(this ValidationContext<T> context, Type validatorType)
            => context.GetValidatorProvider().GetSpecificValidator(validatorType).Validate(context);

        public static Task<ValidationResult> ValidateUsingAsync(this ValidationContext context, Type validatorType, CancellationToken cancellation = default)
            => context.GetValidatorProvider().GetSpecificValidator(validatorType).ValidateAsync(context, cancellation);

        public static Task<ValidationResult> ValidateUsingAsync<T>(this ValidationContext<T> context, Type validatorType, CancellationToken cancellation = default)
            => context.GetValidatorProvider().GetSpecificValidator(validatorType).ValidateAsync(context, cancellation);

        public static ValidationResult ValidateUsing<TValidator>(this ValidationContext context)
            where TValidator : IValidator
            => context.GetValidatorProvider().GetSpecificValidator<TValidator>().Validate(context);

        public static ValidationResult ValidateUsing<T, TValidator>(this ValidationContext<T> context)
            where TValidator : IValidator<T>
            => context.GetValidatorProvider().GetSpecificValidator<TValidator>().Validate(context);

        public static Task<ValidationResult> ValidateUsingAsync<TValidator>(this ValidationContext context, CancellationToken cancellation = default)
            where TValidator : IValidator
            => context.GetValidatorProvider().GetSpecificValidator<TValidator>().ValidateAsync(context, cancellation);

        public static Task<ValidationResult> ValidateUsingAsync<T, TValidator>(this ValidationContext<T> context, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => context.GetValidatorProvider().GetSpecificValidator<TValidator>().ValidateAsync(context, cancellation);

        #endregion

        #region Custom and PropertyValidator context

        //public static TValidator ResolveValidator<TProperty, TValidator>(this CustomContext context)
        //    where TValidator : IValidator<TProperty>
        //{
        //    return GetValidatorProvider(context).GetSpecificValidator<TValidator>();
        //}

        //public static TValidator ResolveValidator<TProperty, TValidator>(this PropertyValidatorContext context)
        //    where TValidator : IValidator<TProperty>
        //{
        //    return GetValidatorProvider(context).GetSpecificValidator<TValidator>();
        //}

        //public static IValidator<TProperty> ResolveValidator<TProperty>(this PropertyValidatorContext context, Type validatorType)
        //{
        //    return (IValidator<TProperty>)GetValidatorProvider(context).GetSpecificValidator(validatorType);
        //}

        //public static IValidator<TProperty> ResolveValidator<TProperty>(this CustomContext context, Type validatorType)
        //{
        //    return (IValidator<TProperty>)GetValidatorProvider(context).GetSpecificValidator(validatorType);
        //}

        //public static IValidator<TProperty> ResolveValidator<TProperty>(this CustomContext context)
        //{
        //    return GetValidatorProvider(context).GetValidator<TProperty>();
        //}

        //public static IValidator<TProperty> ResolveValidator<TProperty>(this PropertyValidatorContext context)
        //{
        //    return GetValidatorProvider(context).GetValidator<TProperty>();
        //}

        //public static TDependency ResolveDependency<TDependency>(this CustomContext context)
        //{
        //    return context.GetServiceProvider().GetRequiredService<TDependency>();
        //}

        //public static TDependency ResolveDependency<TDependency>(this PropertyValidatorContext context)
        //{
        //    return context.GetServiceProvider().GetRequiredService<TDependency>();
        //}

        //public static CustomContext Append(this CustomContext validationContext, ValidationResult result)
        //{
        //    foreach (var failure in result.Errors)
        //        validationContext.AddFailure(failure);
        //    return validationContext;
        //}

        #endregion
    }
}
