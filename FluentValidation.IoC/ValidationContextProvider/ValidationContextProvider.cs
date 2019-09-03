using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider : IValidationContextProvider
    {
        #region Properties

        public IServiceProvider ServiceProvider { get; }

        #endregion

        #region Ctor

        public ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext<T>(T instance)
            => new ValidationContext<T>(instance).WithServiceProvider(ServiceProvider);

        public ValidationContext BuildContext(object instance)
            => new ValidationContext(instance).WithServiceProvider(ServiceProvider);

        public ValidationContext<T> SetupContext<T>(ValidationContext<T> context)
            => context.WithServiceProvider(ServiceProvider);

        public ValidationContext SetupContext(ValidationContext context)
            => context.WithServiceProvider(ServiceProvider);

        #endregion

        #region Validator builders

        public IValidator<T> GetValidator<T>()
            => ServiceProvider.GetValidatorProvider().GetValidator<T>();

        public TValidator GetSpecificValidator<TValidator>()
            where TValidator : IValidator
            => ServiceProvider.GetValidatorProvider().GetSpecificValidator<TValidator>();

        #endregion

        #region Validations

        public ValidationResult Validate<T>(T instance)
            => BuildContext(instance).Validate();

        public ValidationResult Validate<T>(ValidationContext<T> context)
            => SetupContext(context).Validate();

        public ValidationResult ValidateUsing<TValidator>(object instance)
            where TValidator : IValidator
            => BuildContext(instance).ValidateUsing<TValidator>();

        public ValidationResult ValidateUsing<TValidator>(ValidationContext context)
            where TValidator : IValidator
            => SetupContext(context).ValidateUsing<TValidator>();

        public Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation = default)
            => BuildContext(instance).ValidateAsync(cancellation);

        public Task<ValidationResult> ValidateAsync<T>(ValidationContext<T> context, CancellationToken cancellation = default)
            => SetupContext(context).ValidateAsync(cancellation);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, CancellationToken cancellation = default)
            where TValidator : IValidator
            => BuildContext(instance).ValidateUsingAsync<TValidator>(cancellation);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(ValidationContext context, CancellationToken cancellation = default)
            where TValidator : IValidator
            => SetupContext(context).ValidateUsingAsync<TValidator>(cancellation);

        #endregion

        #region Define validated Type

        public IValidationContextProvider<T> For<T>()
            => new ValidationContextProvider<T>(ServiceProvider);

        #endregion
    }
}
