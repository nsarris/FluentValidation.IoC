using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.IoC
{
    internal sealed class ValidationContextProvider<T, TValidator> : IValidationContextProvider<T, TValidator> where TValidator : IValidator<T>
    {
        private readonly IServiceProvider serviceProvider;

        public ValidationContextProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ValidationContext<T> BuildContext(T instance)
            => BuildContext(instance, null);

        private ValidationContext<T> BuildContext(T instance, Action<ValidationContext<T>> configure)
        {
            var context = new ValidationContext<T>(instance);
            context.SetServiceProvider(serviceProvider);
            configure?.Invoke(context);
            return context;
        }

        public ValidationResult Validate(T instance, Action<ValidationContext<T>> configureContext)
            => BuildContext(instance, configureContext).Validate();

        public Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext)
            => BuildContext(instance, configureContext).ValidateAsync(cancellation);

        public Task<ValidationResult> ValidateAsync(T instance, Action<ValidationContext<T>> configureContext)
            => ValidateAsync(instance, default, configureContext);
    }
}
