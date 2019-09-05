using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider<T> : IValidationContextProvider<T>
    {
        #region Fields

        private readonly IServiceProvider serviceProvider;

        #endregion

        #region Ctor

        public ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        #endregion

        #region Context builders

        public ValidationContext<T> BuildContext(T instance)
            => BuildContext(instance, null);

        private ValidationContext<T> BuildContext(T instance, Action<ValidationContext<T>> configure)
        {
            var context = new ValidationContext<T>(instance);
            context.SetServiceProvider(serviceProvider);
            configure?.Invoke(context);
            return context;
        }

        #endregion

        #region Validations

        public ValidationResult Validate(T instance, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).Validate();

        public ValidationResult ValidateUsing(Type validatorType, T instance, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).ValidateUsing(validatorType);

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).ValidateAsync(cancellation);

        public Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null)
            => BuildContext(instance, configureContext).ValidateUsingAsync(validatorType, cancellation);


        public ValidationResult ValidateUsing<TValidator>(T instance, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>
            => ValidateUsing(typeof(TValidator), instance, configureContext);
    
        public Task<ValidationResult> ValidateAsync(T instance, Action<ValidationContext<T>> configureContext = null)
            => ValidateAsync(instance, default, configureContext);

        public Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, Action<ValidationContext<T>> configureContext = null)
            => ValidateUsingAsync(validatorType, instance, default, configureContext);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>
            => ValidateUsingAsync(typeof(TValidator), instance, cancellation, configureContext);

        public Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>
            => ValidateUsingAsync(typeof(TValidator), instance, default, configureContext);

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
