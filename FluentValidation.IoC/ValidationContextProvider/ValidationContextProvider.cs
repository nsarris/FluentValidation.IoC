using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider : IValidationContextProvider
    {
        #region Fields

        public readonly IServiceProvider serviceProvider;

        #endregion

        #region Ctor

        public ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext<T>(T instance)
            => BuildContext<T>(instance, null);

        public ValidationContext BuildContext(object instance)
            => BuildContext(instance, null);

        private ValidationContext<T> BuildContext<T>(T instance, Action<ValidationContext<T>> configure)
        {
            var context = new ValidationContext<T>(instance);
            context.SetServiceProvider(serviceProvider);
            configure?.Invoke(context);
            return context;
        }

        private ValidationContext BuildContext(object instance, Action<ValidationContext> configure)
        {
            var context = new ValidationContext(instance);
            context.SetServiceProvider(serviceProvider);
            configure?.Invoke(context);
            return context;
        }

        #endregion

        #region Validations

        public ValidationResult Validate<T>(T instance, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).Validate();

        public ValidationResult ValidateUsing<TValidator>(object instance, Action<ValidationContext> configureContext = null) where TValidator : IValidator
            => BuildContext(instance, configureContext).ValidateUsing<TValidator>();

        public Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).ValidateAsync(cancellation);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, CancellationToken cancellation, Action<ValidationContext> configureContext = null) where TValidator : IValidator
            => BuildContext(instance, configureContext).ValidateUsingAsync<TValidator>(cancellation);


        public Task<ValidationResult> ValidateAsync<T>(T instance, Action<ValidationContext<T>> configureContext = null)
            => ValidateAsync(instance, default, configureContext);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, Action<ValidationContext> configureContext = null) where TValidator : IValidator
            => ValidateUsingAsync<TValidator>(instance, default, configureContext);

        #endregion

        #region Define validated Type

        public IValidationContextProvider<T> For<T>()
            => new ValidationContextProvider<T>(serviceProvider);

        #endregion
    }
}
