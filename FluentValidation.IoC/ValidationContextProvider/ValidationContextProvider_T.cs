using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider<T> : IValidationContextProvider<T>
    {
        #region Properties

        private readonly IServiceProvider serviceProvider;

        #endregion

        #region Ctor

        internal ValidationContextProvider(IServiceProvider dependencyResolver)
        {
            this.serviceProvider = dependencyResolver;
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext(T instance)
            => new ValidationContext<T>(instance).WithServiceProvider(serviceProvider);

        public ValidationContext<T> SetupContext(ValidationContext<T> context)
            => context.WithServiceProvider(serviceProvider);

        #endregion

        #region Validator builders

        public IValidator<T> GetValidator()
            => serviceProvider.GetValidatorProvider().GetValidator<T>();

        public TValidator GetSpecificValidator<TValidator>()
            where TValidator : IValidator<T>
            => serviceProvider.GetValidatorProvider().GetSpecificValidator<TValidator>();

        #endregion

        #region Validations

        public ValidationResult Validate(T instance)
            => BuildContext(instance).Validate();

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default)
            => BuildContext(instance).ValidateAsync(cancellation);

        public ValidationResult ValidateUsing(Type validatorType, T instance)
            => BuildContext(instance).ValidateUsing(validatorType);

        public ValidationResult ValidateUsing(Type validatorType, ValidationContext<T> context)
            => SetupContext(context).ValidateUsing(validatorType);

        public Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, CancellationToken cancellation = default)
            => BuildContext(instance).ValidateUsingAsync(validatorType, cancellation);

        public Task<ValidationResult> ValidateUsingAsync(Type validatorType, ValidationContext<T> context, CancellationToken cancellation = default)
            => SetupContext(context).ValidateUsingAsync(validatorType, cancellation);

        public ValidationResult ValidateUsing<TValidator>(T instance)
            where TValidator : IValidator<T>
            => BuildContext(instance).ValidateUsing<TValidator>();

        public ValidationResult ValidateUsing<TValidator>(ValidationContext<T> context)
            where TValidator : IValidator<T>
            => SetupContext(context).ValidateUsing<TValidator>();

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => BuildContext(instance).ValidateUsingAsync<TValidator>(cancellation);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(ValidationContext<T> context, CancellationToken cancellation = default)
            where TValidator : IValidator<T>
            => SetupContext(context).ValidateUsingAsync<TValidator>(cancellation);

        #endregion

        #region Define Validator type

        public IValidationContextProvider<T, TValidator> Using<TValidator>()
            where TValidator : IValidator<T>
        {
            return new ValidationContextProvider<T, TValidator>(serviceProvider);
        }

        #endregion
    }
}
