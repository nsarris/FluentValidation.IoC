using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace FluentValidation.IoC
{
    public interface IValidationContextProvider<T>
    {
        ValidationContext<T> BuildContext(T instance);
        
        ValidationResult Validate(T instance, Action<ValidationContext<T>> configureContext = null);
        ValidationResult ValidateUsing(Type validatorType, T instance, Action<ValidationContext<T>> configureContext = null);
        ValidationResult ValidateUsing<TValidator>(T instance, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>;

        Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateAsync(T instance, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateUsingAsync(Type validatorType, T instance, Action<ValidationContext<T>> configureContext = null);
        Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, CancellationToken cancellation, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>;
        Task<ValidationResult> ValidateUsingAsync<TValidator>(T instance, Action<ValidationContext<T>> configureContext = null) where TValidator : IValidator<T>;

        IValidationContextProvider<T, TValidator> Using<TValidator>() where TValidator : IValidator<T>;
    }
}