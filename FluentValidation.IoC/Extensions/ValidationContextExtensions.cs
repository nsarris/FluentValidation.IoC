using FluentValidation.Results;
using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;

namespace FluentValidation.IoC
{
    public static class ValidationContextExtensions
    {
        #region Internal

        private static IValidator GetValidator(this IValidationContext context, Type type, bool assertType)
        {
            if (assertType && !typeof(IValidator).IsAssignableFrom(type))
                throw new InvalidOperationException($"Type {type.Name} does not implement IValidator");

            return (IValidator)context.GetServiceProvider().GetRequiredService(type);
        }

        internal static IValidator GetValidator(this IValidationContext context, Type validatorType)
            => context.GetValidator(validatorType, true);

        internal static IValidator GetValidator<TValidator>(this IValidationContext context)
            where TValidator : IValidator
            => context.GetValidator(typeof(TValidator), false);

        #endregion

        #region ValidationContext

        public static ValidationResult Validate<T>(this ValidationContext<T> context)
            => context.GetValidator<IValidator<T>>().Validate(context);

        public static Task<ValidationResult> ValidateAsync<T>(this ValidationContext<T> context, CancellationToken cancellation = default)
            => context.GetValidator<IValidator<T>>().ValidateAsync(context, cancellation);

        

        public static ValidationResult ValidateUsing<T>(this ValidationContext<T> context, Type validatorType)
            => context.GetValidator(validatorType).Validate(context);

        

        public static Task<ValidationResult> ValidateUsingAsync<T>(this ValidationContext<T> context, Type validatorType, CancellationToken cancellation = default)
            => context.GetValidator(validatorType).ValidateAsync(context, cancellation);

        

        public static ValidationResult ValidateUsing<T, TValidator>(this ValidationContext<T> context)
            where TValidator : IValidator<T>
            => context.GetValidator<TValidator>().Validate(context);

        
        public static Task<ValidationResult> ValidateUsingAsync<T, TValidator>(this ValidationContext<T> context, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => context.GetValidator<TValidator>().ValidateAsync(context, cancellation);

        #endregion
    }
}
