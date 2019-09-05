using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider
    {
        ValidationContext BuildContext(object instance);
        ValidationContext<T> BuildContext<T>(T instance);

        ValidationResult Validate<T>(T instance, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateAsync<T>(T instance, Action<ValidationContext<T>> configureContext = null);
        ValidationResult ValidateUsing<TValidator>(object instance, Action<ValidationContext> configureContext = null) where TValidator : IValidator;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, Action<ValidationContext> configureContext = null) where TValidator : IValidator;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(object instance, CancellationToken cancellation, Action<ValidationContext> configureContext = null) where TValidator : IValidator;

        IValidationContextProvider<T> For<T>();
    }
}